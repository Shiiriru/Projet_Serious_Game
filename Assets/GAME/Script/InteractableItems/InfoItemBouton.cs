using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemBouton : ButtonBase
{
	public InventoryItemInfoObject Infoitem;
	public FicheTemplate ficheTemplate;

	public bool GoToInventory;
	[SerializeField] bool disableObject;

	public override void OnClickButton()
	{
		base.OnClickButton(); //fonction basique + fonction particulière
		//if (disableObject)
		//	ficheTemplate.OpenPageObj(Infoitem, GoToInventory, soundEmitter, this);
		//else
		//	ficheTemplate.OpenPageObj(Infoitem, GoToInventory, soundEmitter); //Je lie ce que j'avais déclaré dans ma parenthèse et je les lie avec ce que j'ai déclaré ici
	}
}
