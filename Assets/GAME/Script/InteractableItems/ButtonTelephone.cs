using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DialogSystem;
using effect;

public class ButtonTelephone : InfoItemBouton
{
	[SerializeField] BureauxMain sceneBereaux;

	[SerializeField] DialogGraph phoneCallDialog;

	[SerializeField] ShakeEffect shakeEffect;
	public ShakeEffect ShakeEffect { get { return shakeEffect; } }


	[SerializeField] DialogGraph[] commandantBrefingDialog;

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		OnChecked();
		SoundPlayer.PlayOneShot(soundEvt);

		if (!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isCommandantCalled"))
		{
			DialogPlayerHelper.SetDialog(commandantBrefingDialog[GameManager.chapterCount]);
			sceneBereaux.AnswerCommandant();
		}

		else
		{
			uiMain.OpenFicheTemplate(Infoitem);
			if (GameManager.chapterCount == 0)
				uiMain.SetFicheTemplateCustomAction(customActionSprite, CallPhone);
		}
	}

	public void CallPhone()
	{
		uiMain.CloseFicheTemplate();
		DialogPlayerHelper.SetDialog(phoneCallDialog);
	}
}
