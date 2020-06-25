using DialogSystem;
using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BureauxMain : SceneManagerBase
{
	[SerializeField] MapGame mapGame;
	[SerializeField] InfoItemBouton BoutonLetter;
	[SerializeField] ButtonBase buttonPhotoVillage;

	[SerializeField] GameObject buttonChangeScene;

	InventoryScript inventory;

	//for hidding other button when not answer commandant
	[SerializeField] GameObject hideOtherInterctableFilter;

	[SerializeField] GameObject photoWall;

	EventInstance phoneRingInstance;
	[SerializeField] [EventRef] string phoneRingEvent;

	[SerializeField] [EventRef] string ambianceEvent;

	protected override void Start()
	{
		inventory = uiMain.UIPlayer.Inventory;

		var commandantCalled = (bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isCommandantCalled");

		if (!commandantCalled)
			uiMain.onChangeSceneFinished += CommandantCall;
		else
			hideOtherInterctableFilter.SetActive(false);

		buttonChangeScene.SetActive(commandantCalled);

		base.Start();
	}

	protected override void SetupChapters()
	{
		base.SetupChapters();
		switch (GameManager.chapterCount)
		{
			case 0:
				mapGame.onGameComplete += () => ActiveBoolean("MapChecked"); //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
				buttonPhotoVillage.onCheckedAction += () => ActiveBoolean("villagePhotoChecked");
				inventory.onAddItem += InventoryItemChecked;
				break;
			case 1:

				break;
			case 2:
				inventory.onAddItem += InventoryItemChecked;
				break;
		}
	}

	private void CommandantCall()
	{
		StartCoroutine(CommandantCallCoroutine());
	}

	IEnumerator CommandantCallCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		uiMain.Ambiance.Play("bg", ambianceEvent);
		if (!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isCommandantCalled"))
			phoneRingInstance.PlayEvent(phoneRingEvent);
	}

	public void AnswerCommandant()
	{
		phoneRingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

		DialogPlayerHelper.SetOnFinishedAction(ActiveExitButton);
		DialogPlayerHelper.VariableSourceMgr.SetValue("isCommandantCalled", true);

		hideOtherInterctableFilter.SetActive(false);
	}

	void InventoryItemChecked(ItemInfoObject item)
	{
		if (item.Id == "informationLetter_" + GameManager.chapterCount)
			DialogPlayerHelper.VariableSourceMgr.SetValue("LetterChecked", true);
		if (item.Id == "badge")
			DialogPlayerHelper.VariableSourceMgr.SetValue("badgeChecked", true);
	}

	private void ActiveBoolean(string boolName)
	{
		DialogPlayerHelper.VariableSourceMgr.SetValue(boolName, true);
	}

	private void ActiveExitButton()
	{
		buttonChangeScene.SetActive(true);
	}

	public void OnClickShowPhotoWall(bool show)
	{
		photoWall.SetActive(show);
	}
}
