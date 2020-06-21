using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem.Nodes;
using XNode;
using System.Linq;

namespace DialogSystem
{
	public class DialogPlayer : MonoBehaviour
	{
		[SerializeField] UIMain uiMain;

		[SerializeField] Image blackBG;
		[SerializeField] TextContent characterNameContent;
		[SerializeField] DialogContent dialogContent;
		[SerializeField] Image cgContent;

		[SerializeField] Transform brancheParent;
		List<BranchButton> brancheBtnList = new List<BranchButton>();

		[SerializeField] BranchButton branchBtnPrefab;

		[SerializeField] VariableSourceManager variableSourceMgr;

		static DialogGraph currentDialogGraph;
		Node currentNode;

		[SerializeField] bool autoClearAction;

		public event System.Action onDialogFinished;

		public bool isPLaying { get; private set; }

		Coroutine waitingCoroutine;

		private void Start()
		{
			Reset();
			DontDestroyOnLoad(gameObject);

			DialogPlayerHelper.SetVariableSourceMgr(variableSourceMgr);
		}

		void Reset()
		{
			uiMain.ShowPlayerUI(false);
			characterNameContent.Show(false);
			dialogContent.Show(false);
			ShowBlackBG(false);

			if (cgContent != null)
			{
				cgContent.color = new Color(1, 1, 1, 0);
				cgContent.raycastTarget = false;
			}
		}

		public void SetDialog(DialogGraph target, bool autoPlay = true)
		{
			if (target == null)
				return;
			currentDialogGraph = target;

			if (autoPlay)
				PlayDialog();
		}

		public void PlayDialog()
		{
			if (currentDialogGraph == null)
				return;

			Reset();
			isPLaying = true;
			currentNode = null;
			AutoPlayNextNode();
		}

		void CurrentDialogFinshed()
		{
			Reset();

			isPLaying = false;
			uiMain.ShowPlayerUI(true);

			if (onDialogFinished != null)
			{
				onDialogFinished();
				if (autoClearAction) onDialogFinished = null;
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (!isPLaying)
				return;

			MouseController();
		}

		private void MouseController()
		{
			//cannot pass dialog when has branch
			if (waitingCoroutine != null || !(currentNode is DialogNode))
				return;

			if (Input.GetMouseButtonDown(0))
			{
				if (dialogContent.isDisplayFinished)
					AutoPlayNextNode();
				else
					dialogContent.DisplayEntierStr();
			}
		}

		//play next story node in current story graph
		void AutoPlayNextNode()
		{
			NodePort connection = null;
			//find  current node
			if (currentNode == null)
				connection = currentDialogGraph.nodes.Where(n => n is StartPoint).FirstOrDefault().GetOutputPort("output").Connection;
			else
				connection = currentDialogGraph.nodes.Where(n => n == currentNode).FirstOrDefault().GetOutputPort("output").Connection;

			PlayNode(connection);
		}

		void AutoPlayNextNode(bool wait, float duration)
		{
			if (wait)
				waitingCoroutine = StartCoroutine(WaitingCorou(duration));
			else
				AutoPlayNextNode();
		}

		public void PlayNode(NodePort connection)
		{
			//finish story
			if (connection == null)
			{
				CurrentDialogFinshed();
				return;
			}

			currentNode = connection.node;

			if (currentNode is DialogNode)
				PlayDialogItem();
			else if (currentNode is HideDialogNode)
				HideDialog();
			else if (currentNode is BranchsNode)
				ShowBranch();

			else if (currentNode is CGNode)
				CGIn();
			else if (currentNode is CGOutNode)
				CGOut();

			else if (currentNode is ConditionBranchNode)
				CheckConditionBranch();

			else if (currentNode is DelayNode)
				DoDelay();
			else if (currentNode is ChangeSceneNode)
				ChangeScene();
			else if (currentNode is RunMethodNode)
				RunMethodOrVariable((currentNode as RunMethodNode).variableMethod, true);

		}

		void PlayDialogItem()
		{
			var node = currentNode as DialogNode;

			if (string.IsNullOrEmpty(node.characterName))
				characterNameContent.Show(false);
			else
			{
				characterNameContent.Show(true);
				characterNameContent.SetText(node.characterName, true);
			}

			if (string.IsNullOrEmpty(node.text))
				dialogContent.Show(false);
			else
			{
				dialogContent.SetDisplaySpeed(node.displaySpeed);
				dialogContent.Show(true);

				dialogContent.SetText(node.text, node.displayAll);
				dialogContent.SetDisplaySide(node.displaySide);
			}

			ShowBlackBG(node.disableScene);
		}

		void ShowBlackBG(bool show)
		{

			if (blackBG.raycastTarget == show)
				return;

			blackBG.DOColor(new Color(0, 0, 0, show ? 0.6f : 0), 0.5f);
			blackBG.raycastTarget = show;
		}

		void HideDialog()
		{
			var node = currentNode as HideDialogNode;
			characterNameContent.Show(false);
			dialogContent.Show(false);
			ShowBlackBG(false);

			AutoPlayNextNode(node.isWait, node.duration);
		}

		void ShowBranch()
		{
			var node = currentNode as BranchsNode;
			//has branch
			if (node.branchs.Count > 0)
			{
				for (int i = 0; i < node.branchs.Count; i++)
				{
					var b = node.branchs[i];

					var connection = node.GetActiveConditions(i);
					//detect branch condition
					if (connection != null && variableSourceMgr != null)
					{
						bool active = true;

						var conditions = connection.node as ActiveConditionsNode;
						//check value
						foreach (var c in conditions.conditions)
						{
							//finish check when one condition isn't correct
							var val1 = c.valueStr.ToValue(c.vType);
							var val2 = variableSourceMgr.GetValue(c.name);
							if (val1 != null && val2 != null && val1.ToString() != val2.ToString())
							{
								active = false;
								continue;
							}
						}
						if (active)
							SetupBrancheButton(b, i);
					}
					//add directly
					else
						SetupBrancheButton(b, i);
				}
			}
			else
				AutoPlayNextNode();
		}

		void SetupBrancheButton(BrancheItem branch, int index)
		{
			BranchButton btn = null;
			//auto add new branche button if out of range
			if (index > brancheBtnList.Count - 1)
				btn = CreateBrancheButton();
			else
				btn = brancheBtnList[index];

			btn.Show(branch);
			var b = branch;
			btn.Button.onClick.AddListener(() => OnClickSelectBranche(b, index));
		}

		void OnClickSelectBranche(BrancheItem branch, int index)
		{
			//refresh all branches
			foreach (var b in brancheBtnList)
				b.Hide(true);

			var connection = (currentNode as BranchsNode).GetOutputConnection(index);

			var returnVal = branch.onSelect;
			RunMethodOrVariable(returnVal, false);
			PlayNode(connection);
		}

		void RunMethodOrVariable(VariableMethodItem item, bool playNextNode)
		{
			if (variableSourceMgr != null && item.vType != VariableType.Null)
			{
				if (item.vType == VariableType.Method)
					variableSourceMgr.PlayMethod(item.method);
				else
					variableSourceMgr.SetValue(item.variable);
			}

			if (playNextNode)
				AutoPlayNextNode();
		}

		BranchButton CreateBrancheButton()
		{
			var btn = Instantiate<BranchButton>(branchBtnPrefab);
			btn.transform.SetParent(brancheParent, false);

			brancheBtnList.Add(btn);
			return btn;
		}

		void CGIn()
		{
			if (cgContent == null)
			{
				AutoPlayNextNode();
				return;
			}

			var node = currentNode as CGNode;

			var duration = node.duration;
			if (node.duration > 0)
			{
				DOTween.Kill(cgContent);
				if (cgContent.color.a > 0)
					cgContent.DOColor(Color.black, duration * 0.5f).OnComplete(() =>
					{
						cgContent.sprite = node.sprite;
						cgContent.DOColor(Color.white, duration * 0.5f);
					});
				else
				{
					cgContent.sprite = node.sprite;
					cgContent.DOColor(Color.white, duration);
				}

			}
			else
			{
				cgContent.DOColor(Color.white, duration);
				cgContent.color = Color.white;
			}
			cgContent.raycastTarget = true;

			AutoPlayNextNode(node.isWait, node.duration);
		}

		void CGOut()
		{
			if (cgContent == null)
			{
				AutoPlayNextNode();
				return;
			}

			var node = currentNode as CGOutNode;

			DOTween.Kill(cgContent);
			cgContent.DOColor(new Color(1, 1, 1, 0), node.duration);
			cgContent.raycastTarget = false;

			AutoPlayNextNode(node.isWait, node.duration);
		}

		void DoDelay()
		{
			var item = currentNode as DelayNode;
			waitingCoroutine = StartCoroutine(WaitingCorou(item.duration));
		}

		void ChangeScene()
		{
			var node = currentNode as ChangeSceneNode;
			Reset();
			uiMain.ChangeScene(node.scene.name, node.duration, node.dateInfos);
		}

		private void CheckConditionBranch()
		{
			var node = currentNode as ConditionBranchNode;

			if (node.branchs.Count < 1)
			{
				AutoPlayNextNode();
				return;
			}

			var value = variableSourceMgr.GetValue(node.condition);
			for (int i = 0; i < node.branchs.Count; i++)
			{
				if (value.Equals(node.branchs[i].ToValue(node.condition.vType)))
				{
					var connection = node.GetOutputConnection(i);
					PlayNode(connection);
					break;
				}
			}
		}

		public void SetVariableSourceManger(VariableSourceManager mgr)
		{
			variableSourceMgr = mgr;
			DialogPlayerHelper.SetVariableSourceMgr(variableSourceMgr);
		}

		IEnumerator WaitingCorou(float time)
		{
			yield return new WaitForSeconds(time);
			AutoPlayNextNode();
			waitingCoroutine = null;
		}
	}
}