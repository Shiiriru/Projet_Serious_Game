using UnityEngine;

namespace DialogSystem.Demo
{
	public class DialogDemo : MonoBehaviour
	{
		DialogListManager manager;
		DialogPlayer dialogPlayer;

		// Start is called before the first frame update
		void Start()
		{
			manager = GetComponent<DialogListManager>();
			dialogPlayer = FindObjectOfType<DialogPlayer>();

			dialogPlayer.SetDialog(manager.dialogList[0]);
			dialogPlayer.onPlayNextDialogAction += NextDialog;
			dialogPlayer.onSelectBrancheAction += SelectedBrancheAction;
		}

		private void NextDialog()
		{
			dialogPlayer.SetDialog(manager.dialogList[1]);
		}

		void SelectedBrancheAction(BrancheItem branch)
		{
			//var value = dialogPlayer.ConditionSource.GetValue(brancheValue.variableName);
			//Debug.Log(value);
		}
	}
}