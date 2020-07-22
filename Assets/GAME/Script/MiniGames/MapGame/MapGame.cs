using DialogSystem;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MapGame : MiniGameBase
{
	[SerializeField] MapGamePawn[] pawnList;

	[SerializeField] DialogGraph dialog;
	[EventRef] public string completeEvt;

	private void Start()
	{
		bool mapChecked = (bool)DialogPlayerHelper.VariableSourceMgr.GetValue("MapChecked");
		foreach (var p in pawnList)
		{
			//auto set pawn pos
			if (mapChecked)
			{
				p.SetPosToTargetCase();
			}				
			else
			{
				DialogPlayerHelper.SetDialog(dialog);
				p.Init(this);
			}				
		}
	}

	public void CheckMapCorrect()
	{
		//return if one pawn isn't in the right place
		foreach (var p in pawnList)
		{
			if (!p.isCorrect)
				return;
		}

		OnGameComplete();
		StartCoroutine(GameFinishCoroutine());
	}

	IEnumerator GameFinishCoroutine()
	{
		yield return new WaitForSeconds(0.3f);
		SoundPlayer.PlayOneShot(completeEvt);
		yield return new WaitForSeconds(1f);
		DialogPlayerHelper.SetDialog(dialog);
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
