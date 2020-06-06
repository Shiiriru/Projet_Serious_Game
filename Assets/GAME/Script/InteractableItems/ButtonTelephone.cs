using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DialogSystem;

public class ButtonTelephone : InfoItemBouton
{
	bool isCommandantCalled;

	[SerializeField] DialogGraph commandantDialog;
	[SerializeField] DialogGraph phoneCallDialog;

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		base.OnClickButton();

		if (!isCommandantCalled)
		{
			DialogPlayerHelper.SetDialog(commandantDialog);
			isCommandantCalled = true;
		}
		else
		{
			var actions = new Dictionary<string, System.Action>();
			actions.Add("Appeler quelqu'un", CallPhone);
			uiMain.OpenFicheTemplate(Infoitem, false, soundEmitter, customActions: actions);
		}
	}

	public void CallPhone()
	{
		uiMain.CloseFicheTemplate();
		DialogPlayerHelper.SetDialog(phoneCallDialog);
	}
}
