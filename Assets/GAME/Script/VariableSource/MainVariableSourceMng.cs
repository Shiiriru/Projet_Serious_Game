using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainVariableSourceMng : VariableSourceManager, IMainVariableSource
{
	public MainVariableSource source = new MainVariableSource();

	[SerializeField] InventoryScript inventory;

	OnBoardingMain onBoardingMain;
	//BureauxMain bureauMain;
	TrencheMain trencheMain;

	#region Values function
	public override FieldInfo[] GetSourceFieldInfo()
	{
		return source.GetFieldInfos();
	}

	public override object GetValue(string name)
	{
		return source.GetValue(name);
	}

	public override object GetValue(VariableObject obj)
	{
		return source.GetValue(obj);
	}

	public override void SetValue(VariableObject variable)
	{
		source.SetValue(variable.name, variable.valueStr);
	}

	public override void SetValue(string variableName, object value)
	{
		source.SetValue(variableName, value);
	}

	public override void PlayMethod(MethodObject methodObj)
	{
		Type type = this.GetType();
		MethodInfo method = type.GetMethod(methodObj.name);
		//has parameter
		if (methodObj.hasParameter)
			method.Invoke(this, new[] { methodObj.parameter.value });
		else
			method.Invoke(this, null);

	}
	#endregion

	private void Start()
	{
		SceneManager.sceneLoaded += FindSceneManager;
		FindSceneManager(SceneManager.GetActiveScene(), LoadSceneMode.Single);
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= FindSceneManager;
	}

	private void FindSceneManager(Scene arg0, LoadSceneMode arg1)
	{
		var main = FindObjectOfType<SceneManagerBase>();
		if (main is OnBoardingMain)
			onBoardingMain = main as OnBoardingMain;
		//if (main is BureauxMain)
		//	bureauMain = main as BureauxMain;
		else if (main is TrencheMain)
			trencheMain = main as TrencheMain;
	}

	public void OnBordingSelectPhotoFace(bool smile)
	{
		onBoardingMain.ShowPhoto(smile);
		source.isPhotoSmile = smile;
	}

	public void LaunchGameSpotGerman()
	{
		trencheMain.LaunchSpotGerman();
	}

	public void LaunchCardGame()
	{
		trencheMain.LaunchBlackJack();
	}

	public void LootWatch()
	{
		trencheMain.LootWatch();
	}

	public void LootMask()
	{
		trencheMain.LootMask();
		source.MaskChecked = true;
	}

	public void SendLetter(int chapter)
	{
		source.letterSent = true;
		source.LetterChecked = false;
		inventory.Remove("informationLetter_" + chapter);
	}

	public void AfterGasAttack()
	{
		trencheMain.AfterGasAttack();
	}

	public void LoseInventoryObject(string id)
	{
		inventory.Remove(id);
	}

	public void WearMask(bool newMask)
	{
		source.isUsedNewMask = newMask;
		trencheMain.WearMask(newMask);
	}

	public void LaunchFinalWar()
	{
		trencheMain.LaunchFinalWar();
	}
}
