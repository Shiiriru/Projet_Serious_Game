using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem
{
	public class DialogPlayer : MonoBehaviour
	{
		[SerializeField] Text characterNameContent;
		[SerializeField] Text dialogContent;

		[SerializeField] Transform brancheParent;
		List<BranchButton> brancheBtnList = new List<BranchButton>();

		[SerializeField] BranchButton branchBtnPrefab;

		[SerializeField] VariableSourceManager variableSourceMgr;
		public VariableSourceManager VariableSourceMgr { get { return variableSourceMgr; } }

		DialogGroupItem targetDialog;

		[SerializeField] bool autoClearAction;
		public event System.Action onPlayNextDialogAction;
		public event System.Action<BrancheItem> onSelectBrancheAction;

		bool startPlayDialog;
		//for play dialog one by one
		int nowPlayIndex;
		int nextPlayIndex;

		bool showBranches;

		public void SetDialog(DialogGroupItem target, bool autoPlay = true)
		{
			targetDialog = target;

			if (autoPlay)
			{
				startPlayDialog = true;
				nowPlayIndex = -1;
				nextPlayIndex = 0;
			}
		}

		public void PlayDialog()
		{
			if (targetDialog == null)
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

			if (nextPlayIndex >= targetDialog.dialogList.Count)
				CurrentDialogFinshed();

			Play();
			MouseController();
		}

		private void MouseController()
		{
			//cannot pass dialog when has branch
			if (showBranches)
				return;

			if (Input.GetMouseButtonDown(0))
			{
				nextPlayIndex += 1;
			}
		}

		void Play()
		{
			if (nowPlayIndex == nextPlayIndex || nextPlayIndex >= targetDialog.dialogList.Count)
				return;

			nowPlayIndex = nextPlayIndex;
			var dialog = targetDialog.dialogList[nowPlayIndex];

			characterNameContent.text = string.IsNullOrEmpty(dialog.characterName) ? "" : dialog.characterName;
			dialogContent.text = string.IsNullOrEmpty(dialog.text) ? "" : dialog.text;

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
			characterNameContent.text = dialogContent.text = "";
			startPlayDialog = false;
			if (onPlayNextDialogAction != null)
			{
				onPlayNextDialogAction();
				if (autoClearAction) onPlayNextDialogAction = null;
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

			if (onSelectBrancheAction != null)
			{
				onSelectBrancheAction(branch);
				if (autoClearAction) onSelectBrancheAction = null;
			}
		}

		BranchButton CreateBrancheButton()
		{
			var btn = Instantiate<BranchButton>(branchBtnPrefab);
			btn.transform.SetParent(brancheParent, false);

			brancheBtnList.Add(btn);
			return btn;
		}
	}
}
