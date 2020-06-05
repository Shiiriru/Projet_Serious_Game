using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SceneBureauxManager : MonoBehaviour
{
	[SerializeField] ButtonBase BoutonMap;
	[SerializeField] InfoItemBouton BoutonLetter;
	[SerializeField] InfoItemBouton BoutonPhoto;

	[SerializeField] GameObject buttonChangeScene;
	DialogPlayer dialogPlayer;
	UIMain uiMain;
	InventoryScript inventory;

	[SerializeField] InfoItemBouton[] infoButtonList;

	// Start is called before the first frame update
	void Start()
	{
		dialogPlayer = FindObjectOfType<DialogPlayer>();

		uiMain = FindObjectOfType<UIMain>();
		uiMain.ShowPlayerUI(true);

		inventory = uiMain.UIPlayer.Inventory;

		foreach (var b in infoButtonList)
			b.Init(uiMain);

		BoutonMap.OnChecked += CheckMapBoolean; //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
		BoutonPhoto.OnChecked += CheckPhotoBoolean;

		inventory.onItemChanged += ActiveExitButton;
	}

	private void CheckMapBoolean()
	{
		dialogPlayer.VariableSourceMgr.SetValue("MapIsChecked", true);

		ActiveExitButton();
	}

	private void CheckArtileryBoolean()
	{
		dialogPlayer.VariableSourceMgr.SetValue("ArtileryIsChecked", true);
		ActiveExitButton();
	}

	private void CheckPhotoBoolean()
	{
		dialogPlayer.VariableSourceMgr.SetValue("PhotoIsChecked", true);
	}

	private void ActiveExitButton()
	{
		if (!inventory.CheckHasObject("informationLetter") ||
			!(bool)dialogPlayer.VariableSourceMgr.GetValue("ArtileryIsChecked"))
			return;

		buttonChangeScene.SetActive(true);
	}
}
