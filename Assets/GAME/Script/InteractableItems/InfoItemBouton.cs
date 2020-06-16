using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemBouton : ButtonBase
{
	[SerializeField] protected ItemInfoObject Infoitem;
	[SerializeField] protected Sprite customActionSprite;

	public override void OnClickButton()
	{
		base.OnClickButton(); //fonction basique + fonction particulière

		uiMain.OpenFicheTemplate(Infoitem, soundEmitter);
	}
}
