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

		uiMain.OpenFicheTemplate(Infoitem, this, GoToInventory, soundEmitter);
	}

	public virtual void AddToInventory()
	{
		if (disableObject)
			gameObject.SetActive(false);
		GoToInventory = false;
	}
}
