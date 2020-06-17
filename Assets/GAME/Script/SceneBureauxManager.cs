using DialogSystem;
using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SceneBureauxManager : MonoBehaviour
{
	[SerializeField] MapGame mapGame;
	[SerializeField] InfoItemBouton BoutonLetter;
	[SerializeField] ButtonBase buttonPhotoVillage;

	[SerializeField] GameObject buttonChangeScene;
	UIMain uiMain;
	InventoryScript inventory;

	public bool IsCommandantCalled { get; private set; }
	[SerializeField] DialogGraph commandantDialog;
	//for hidding other button when not answer commandant
	[SerializeField] GameObject hideOtherInterctableFilter;

	[SerializeField] GameObject photoWall;

	EventInstance phoneRingInstance;
	[SerializeField] [EventRef] string phoneRingEvent;

	[SerializeField] [EventRef] string ambianceEvent;

	// Start is called before the first frame update
	void Start()
	{
		uiMain = FindObjectOfType<UIMain>();
		uiMain.onChangeSceneFinished += CommandantCall;

		inventory = uiMain.UIPlayer.Inventory;

		mapGame.onGameComplete += () => CheckBoolean("MapChecked"); //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
		buttonPhotoVillage.onCheckedAction += () => CheckBoolean("villagePhotoChecked");

		inventory.onItemChanged += ActiveExitButton;
	}

	private void CommandantCall()
	{
		StartCoroutine(CommandantCallCoroutine());
	}

	IEnumerator CommandantCallCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		uiMain.Ambiance.Play(ambianceEvent);
		yield return new WaitForSeconds(0.5f);
		phoneRingInstance.PlayEvent(phoneRingEvent);
	}

	public void AnswerCommandant()
	{
		phoneRingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

		DialogPlayerHelper.SetDialog(commandantDialog);
		IsCommandantCalled = true;
		hideOtherInterctableFilter.SetActive(false);
	}

	private void CheckBoolean(string boolName)
	{
		DialogPlayerHelper.VariableSourceMgr.SetValue(boolName, true);
		ActiveExitButton();
	}

	private void ActiveExitButton()
	{
		if (!inventory.CheckHasObject("informationLetter") ||
			!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("MapChecked"))
			return;

		DialogPlayerHelper.VariableSourceMgr.SetValue("LetterChecked", true);
		buttonChangeScene.SetActive(true);
	}

	public void OnClickShowPhotoWall(bool show)
	{
		photoWall.SetActive(show);
	}
}
