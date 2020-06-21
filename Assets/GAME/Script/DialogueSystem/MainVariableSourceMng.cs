using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MainVariableSourceMng : VariableSourceManager, IMainVariableSource
{
	public MainVariableSource source = new MainVariableSource();

	[SerializeField] InventoryScript inventory;

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

	public void OnBordingSelectPhotoFace(bool smile)
	{
		FindObjectOfType<OnBoardingMain>().ShowPhoto(smile);
		source.isPhotoSmile = smile;
	}

	public void LaunchGameSpotGerman()
	{
		FindObjectOfType<TrencheMain>().LaunchSpotGerman();
	}

	public void LaunchCardGame()
	{
		FindObjectOfType<TrencheMain>().LaunchBlackJack();
	}

	public void LootWatch()
	{
		FindObjectOfType<TrencheMain>().LootWatch();
	}

	public void LootMask()
	{
		FindObjectOfType<TrencheMain>().LootMask();
		source.MaskChecked = true;
	}

	public void SendLetter(int chapter)
	{
		source.letterSent = true;
		source.LetterChecked = false;
		inventory.Remove("informationLetter_" + chapter);
	}

	public void LoseInventoryObject(string id)
	{
		inventory.Remove(id);
	}
}
