using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemBouton : ButtonBase
{
	[SerializeField] protected InventoryItemInfoObject Infoitem;
	protected UIMain uiMain;

	public bool GoToInventory;
	[SerializeField] bool disableObject;

	public void Init(UIMain _uiMain)
	{
		uiMain = _uiMain;
	}

	public override void OnClickButton()
	{
		base.OnClickButton(); //fonction basique + fonction particulière
		if (disableObject)
			uiMain.OpenFicheTemplate(Infoitem, GoToInventory, soundEmitter, this);
		else
			uiMain.OpenFicheTemplate(Infoitem, GoToInventory, soundEmitter); //Je lie ce que j'avais déclaré dans ma parenthèse et je les lie avec ce que j'ai déclaré ici
	}

	public virtual void Disable()
	{
		GoToInventory = false;
		gameObject.SetActive(false);
	}
}
