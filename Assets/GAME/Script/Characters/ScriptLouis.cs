using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;

public class ScriptLouis : CharacterBase
{
	[SerializeField] DialogGraph dialogSpotFinish;

	protected override void OnClickPlayDialog()
	{
		//must talk to louis
		DialogPlayerHelper.SetOnFinishedAction(()=> { DialogPlayerHelper.VariableSourceMgr.SetValue("isTalkedToLouis", true); });

		switch (GameManager.chapterCount)
		{
			case 0:
				DialogPlayerHelper.SetOnFinishedAction(Leave);
				base.OnClickPlayDialog();
				break;
			case 1:
			case 2:
				base.OnClickPlayDialog();
				break;
		}		
	}

	void Leave()
	{
		bool leave = (bool)DialogPlayerHelper.VariableSourceMgr.GetValue("louisLeave");
		if (leave)
			gameObject.SetActive(false);
	}

	public void SpotFinish()
	{
		DialogPlayerHelper.SetDialog(dialogSpotFinish);
		DialogPlayerHelper.SetOnFinishedAction(() => { DialogPlayerHelper.VariableSourceMgr.SetValue("spotGermanFinished", true); });
	}
}
