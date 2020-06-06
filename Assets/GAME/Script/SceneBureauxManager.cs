using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SceneBureauxManager : MonoBehaviour
{
	[SerializeField] MapGame mapGame;
	[SerializeField] InfoItemBouton BoutonLetter;
	[SerializeField] InfoItemBouton BoutonPhoto;

	[SerializeField] GameObject buttonChangeScene;
	UIMain uiMain;
	InventoryScript inventory;

	[SerializeField] InfoItemBouton[] infoButtonList;

	// Start is called before the first frame update
	void Start()
	{
		uiMain = FindObjectOfType<UIMain>();
		uiMain.ShowPlayerUI(true);
		uiMain.onChangeSceneFinished += CommandantCall;

		inventory = uiMain.UIPlayer.Inventory;

		foreach (var b in infoButtonList)
			b.Init(uiMain);

		mapGame.onGameComplete += CheckMapBoolean; //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
		BoutonPhoto.OnChecked += CheckPhotoBoolean;

		inventory.onItemChanged += ActiveExitButton;
	}

	private void CommandantCall()
	{
		StartCoroutine(CommandantCallCoroutine());
	}

	IEnumerator CommandantCallCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		//play sound
	}

	private void CheckMapBoolean()
	{
		DialogPlayerHelper.VariableSourceMgr.SetValue("MapChecked", true);
		ActiveExitButton();
	}

	private void CheckPhotoBoolean()
	{
		DialogPlayerHelper.VariableSourceMgr.SetValue("PhotoChecked", true);
	}

	private void ActiveExitButton()
	{
		if (!inventory.CheckHasObject("informationLetter") ||
			!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("MapChecked"))
			return;

		DialogPlayerHelper.VariableSourceMgr.SetValue("LetterChecked", true);
		buttonChangeScene.SetActive(true);
	}
}
