﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DialogSystem;

public class ButtonTelephone : InfoItemBouton
{
	[SerializeField] SceneBureauxManager sceneBereaux;

	[SerializeField] DialogGraph phoneCallDialog;

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		OnChecked();
		SoundPlayer.PlayOneShot(soundEvt);

		if (!sceneBereaux.IsCommandantCalled)
			sceneBereaux.AnswerCommandant();
		else
		{
			uiMain.OpenFicheTemplate(Infoitem);
			uiMain.SetFicheTemplateCustomAction(customActionSprite, CallPhone);
		}
	}

	public void CallPhone()
	{
		uiMain.CloseFicheTemplate();
		DialogPlayerHelper.SetDialog(phoneCallDialog);
	}
}
