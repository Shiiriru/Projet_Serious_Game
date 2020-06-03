using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem
{
	public class DialogPlayer : MonoBehaviour
	{
		[SerializeField] TextContent characterNameContent;
		[SerializeField] TextContent dialogContent;
		[SerializeField] Image cgContent;

		[SerializeField] Transform brancheParent;
		List<BranchButton> brancheBtnList = new List<BranchButton>();

		[SerializeField] BranchButton branchBtnPrefab;

		[SerializeField] VariableSourceManager variableSourceMgr;
		public VariableSourceManager VariableSourceMgr { get { return variableSourceMgr; } }

		DialogGroupItem currentGroup;
		DialogItemBase currentItem;

		[SerializeField] bool autoClearAction;

		public event System.Action<DialogItemBase> onPlayItem;
		public event System.Action onDialogFinished;
		public event System.Action<BrancheItem> onSelectBranche;

		bool startPlayDialog;
		//for play dialog one by one
		int nowPlayIndex;
		int nextPlayIndex;

		bool showBranches;
		Coroutine waitingCoroutine;

		private void Awake()
		{
			Reset();

			DontDestroyOnLoad(gameObject);
		}

		void Reset()
		{
			if (characterNameContent != null)
				characterNameContent.Show(false);
			if (dialogContent != null)
				dialogContent.Show(false);

			if (cgContent != null)
			{
				cgContent.color = new Color(1, 1, 1, 0);
				cgContent.raycastTarget = false;
			}
		}

		public void SetDialog(DialogGroupItem target, bool autoPlay = true)
		{
			currentGroup = target;

			if (autoPlay)
			{
				startPlayDialog = true;
				nowPlayIndex = -1;
				nextPlayIndex = 0;
			}
		}

		public void PlayDialog()
		{
			if (currentGroup == null)
				return;

			startPlayDialog = true;
			nowPlayIndex = -1;
			nextPlayIndex = 0;
		}

		// Update is called once per frame
		void Update()
		{
			if (!startPlayDialog)
				return;

			if (nextPlayIndex >= currentGroup.itemList.Count)
				CurrentDialogFinshed();

			Play();
			MouseController();
		}

		private void MouseController()
		{
			//cannot pass dialog when has branch
			if (showBranches || waitingCoroutine != null || !(currentItem is DialogItem))
				return;

			if (Input.GetMouseButtonDown(0))
				nextPlayIndex += 1;
		}

		void Play()
		{
			if (nowPlayIndex == nextPlayIndex || nextPlayIndex >= currentGroup.itemList.Count)
				return;

			nowPlayIndex = nextPlayIndex;
			currentItem = currentGroup.itemList[nowPlayIndex];

			if (onPlayItem != null)
			{
				onPlayItem(currentItem);
				if (autoClearAction) onPlayItem = null;
			}

			if (currentItem is DialogItem)
				PlayDialogItem();
			if (currentItem is CGItem)
				CGIn();
			if (currentItem is CGOutItem)
				CGOut();
			if (currentItem is DelayItem)
				DoDelay();
		}

		void PlayDialogItem()
		{
			var dialog = currentItem as DialogItem;

			if (characterNameContent != null)
			{
				if (string.IsNullOrEmpty(dialog.characterName))
					characterNameContent.Show(false);
				else
				{
					characterNameContent.Show(true);
					characterNameContent.SetText(dialog.characterName);
				}
			}

			if (dialogContent != null)
			{
				if (string.IsNullOrEmpty(dialog.text))				
					dialogContent.Show(false);			
				else
				{
					dialogContent.Show(true);
					dialogContent.SetText(dialog.text);
				}
			}


			//has branch
			if (dialog.branches.Count > 0)
			{
				for (int i = 0; i < dialog.branches.Count; i++)
				{
					var b = dialog.branches[i];
					//detect branch condition
					if (b.activeConditions.Count > 0 && variableSourceMgr != null)
					{
						bool active = true;
						//check value 
						foreach (var condition in b.activeConditions)
						{
							//finish check when one condition isn't correct
							var val1 = condition.valueStr.ToValue(condition.vType.ToType());
							var val2 = variableSourceMgr.GetValue(condition.name);
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
			btn.Button.onClick.AddListener(() => OnClickSelectBranche(b));

			showBranches = true;
		}

		void OnClickSelectBranche(BrancheItem branch)
		{
			//refresh all branches
			foreach (var b in brancheBtnList)
				b.Hide(true);
			showBranches = false;

			nextPlayIndex += 1;

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
				nextPlayIndex += 1;
				return;
			}

			var item = currentItem as CGItem;

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

			PlayNextItem(item.waitFade, item.duration);
		}

		void CGOut()
		{
			if (cgContent == null)
			{
				nextPlayIndex += 1;
				return;
			}

			var item = currentItem as CGOutItem;

			DOTween.Kill(cgContent);
			cgContent.DOColor(new Color(1, 1, 1, 0), item.duration);
			cgContent.raycastTarget = false;

			PlayNextItem(item.waitFade, item.duration);
		}

		void PlayNextItem(bool wait, float duration)
		{
			if (wait)
				waitingCoroutine = StartCoroutine(WaitingCorou(duration));
			else
				nextPlayIndex += 1;
		}

		void DoDelay()
		{
			var item = currentItem as DelayItem;
			waitingCoroutine = StartCoroutine(WaitingCorou(item.duration));
		}

		public void SetVariableManger(VariableSourceManager mgr)
		{
			variableSourceMgr = mgr;
		}

		IEnumerator WaitingCorou(float time)
		{
			yield return new WaitForSeconds(time);
			nextPlayIndex += 1;
			waitingCoroutine = null;
		}
	}
}
