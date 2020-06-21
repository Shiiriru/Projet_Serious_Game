using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayers : CharacterBase
{
	[SerializeField] DialogGraph dialogGameResult;
	[SerializeField] DialogGraph dialogAfterGame;

	static bool gameFinished;

	protected override void OnClickPlayDialog()
	{
		DialogPlayerHelper.SetDialog(gameFinished ? dialogAfterGame : dialog);
	}

	public void LaunchResultDialog()
	{
		gameFinished = true;
		DialogPlayerHelper.SetDialog(dialogGameResult);
	}
}
