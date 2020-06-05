using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem.Nodes;
using XNode;
using System.Linq;
using FMODUnity;

namespace DialogSystem
{
	public class DialogPlayer : MonoBehaviour
	{
		[SerializeField] UIInventory uiInventory;

		[SerializeField] Image blackBG;
		[SerializeField] TextContent characterNameContent;
		[SerializeField] TextContent dialogContent;
		[SerializeField] Image cgContent;

		[SerializeField] Transform brancheParent;
		List<BranchButton> brancheBtnList = new List<BranchButton>();

		[SerializeField] BranchButton branchBtnPrefab;

		[SerializeField] DatePanel datePanel;
		public DatePanel DatePanel { get { return datePanel; } }

		[SerializeField] VariableSourceManager variableSourceMgr;
		public VariableSourceManager VariableSourceMgr { get { return variableSourceMgr; } }

		DialogGraph currentDialogGraph;
		Node currentNode;

		[SerializeField] bool autoClearAction;

		public event System.Action<Node> onPlayNode;
		public event System.Action onDialogFinished;
		public event System.Action<BrancheItem> onSelectBranche;

		bool startPlayDialog;

		Coroutine waitingCoroutine;

		[SerializeField] [EventRef] string soundSwithScene;

		private void Start()
		{
			Reset();
			DontDestroyOnLoad(gameObject);
		}

		void Reset()
		{
			uiInventory.Show(false);
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
			currentDialogGraph = target;

			if (autoPlay)
				PlayDialog();
		}

		public void PlayDialog()
		{
			if (currentDialogGraph == null)
				return;

			startPlayDialog = true;
			currentNode = null;
			AutoPlayNextNode();
		}

		void CurrentDialogFinshed()
		{
			Reset();

			startPlayDialog = false;
			if (onDialogFinished != null)
			{
				onDialogFinished();
				if (autoClearAction) onDialogFinished = null;
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (!startPlayDialog)
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

			if (onPlayNode != null)
			{
				onPlayNode(currentNode);
				if (autoClearAction) onPlayNode = null;
			}

			if (currentNode is DialogNode)
				PlayDialogItem();
			if (currentNode is BranchsNode)
				ShowBranch();
			if (currentNode is CGNode)
				CGIn();
			if (currentNode is CGOutNode)
				CGOut();
			if (currentNode is DelayNode)
				DoDelay();
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
							var val1 = c.valueStr.ToValue(c.vType.ToType());
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
			if (variableSourceMgr != null && returnVal.vType != VariableType.Null)
			{
				if (returnVal.vType == VariableType.Method)
					variableSourceMgr.PlayMethod(returnVal.method);
				else
					variableSourceMgr.SetValue(returnVal.variable);
			}

			if (onSelectBranche != null)
			{
				onSelectBranche(branch);
				if (autoClearAction) onSelectBranche = null;
			}

			PlayNode(connection);
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

			var item = currentNode as CGNode;

			var duration = item.duration;
			if (item.duration > 0)
			{
				DOTween.Kill(cgContent);
				if (cgContent.color.a > 0)
					cgContent.DOColor(Color.black, duration * 0.5f).OnComplete(() =>
					{
						cgContent.sprite = item.sprite;
						cgContent.DOColor(Color.white, duration * 0.5f);
					});
				else
				{
					cgContent.sprite = item.sprite;
					cgContent.DOColor(Color.white, duration);
				}

			}
			else
			{
				cgContent.DOColor(Color.white, duration);
				cgContent.color = Color.white;
			}
			cgContent.raycastTarget = true;

			AutoPlayNextNode(item.isWait, item.duration);
		}

		void CGOut()
		{
			if (cgContent == null)
			{
				AutoPlayNextNode();
				return;
			}

			var item = currentNode as CGOutNode;

			DOTween.Kill(cgContent);
			cgContent.DOColor(new Color(1, 1, 1, 0), item.duration);
			cgContent.raycastTarget = false;

			AutoPlayNextNode(item.isWait, item.duration);
		}

		void DoDelay()
		{
			var item = currentNode as DelayNode;
			waitingCoroutine = StartCoroutine(WaitingCorou(item.duration));
		}

		public void SetVariableManger(VariableSourceManager mgr)
		{
			variableSourceMgr = mgr;
		}

		IEnumerator WaitingCorou(float time)
		{
			yield return new WaitForSeconds(time);
			AutoPlayNextNode();
			waitingCoroutine = null;
		}
	}
}