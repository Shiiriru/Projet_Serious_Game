using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MainVariableSourceMng : VariableSourceManager, IMainVariableSource
{
	public MainVariableSource source = new MainVariableSource();

	#region Values function
	public override FieldInfo[] GetSourceFieldInfo()
	{
		return source.GetFieldInfos();
	}

	public override object GetValue(string name)
	{
		return source.GetValue(name);
	}

	public override object GetValue(object obj)
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
		MethodInfo method = type.GetMethod((string)methodObj.name);
		//has parameter
		if (methodObj.hasParameter)
			method.Invoke(this, new[] { methodObj.parameter.value });
		else
			method.Invoke(this, null);
	}
	#endregion

	public void OnBordingSelectPhotoFace(int index)
	{
		FindObjectOfType<OnBoardingMain>().afficherPhoto(index);
	}
}
