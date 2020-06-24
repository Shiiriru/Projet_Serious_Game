using DialogSystem;
using UnityEngine;

public class MapGame : MiniGameBase
{
	[SerializeField] MapGamePawn[] pawnList;

	[SerializeField] DialogGraph dialog;

	private void Start()
	{
		bool mapChecked = (bool)DialogPlayerHelper.VariableSourceMgr.GetValue("MapChecked");
		foreach (var p in pawnList)
		{
			//auto set pawn pos
			if (mapChecked)
				p.SetPosToTargetCase();
			else
			{
				DialogPlayerHelper.SetDialog(dialog);
				p.onHoverEnd += CheckMapCorrect;
			}				
		}
	}

	private void CheckMapCorrect()
	{
		//return if one pawn isn't in the right place
		foreach (var p in pawnList)
		{
			if (!p.CheckCorrect())
				return;
		}

		OnGameComplete();
		DialogPlayerHelper.SetDialog(dialog);
		DialogPlayerHelper.SetOnFinishedAction(OnClickClose);
	}

	public void OnClickClose()
	{
		Show(false);
	}

	public void Show(bool show)
	{
		gameObject.SetActive(show);
	}
}
