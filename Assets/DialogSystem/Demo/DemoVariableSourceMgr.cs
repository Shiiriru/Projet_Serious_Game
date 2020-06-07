using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DialogSystem.Demo
{
	//monobehavior
	//runtime
	public class DemoVariableSourceMgr : VariableSourceManager, ISoldatVariableSource
	{
		SoldatVariableSource source = new SoldatVariableSource();

		//private void Start()
		//{
		//	source.activeChoice1 = true;
		//}

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
			MethodInfo method = type.GetMethod((string)methodObj.name);
			//has parameter
			if (methodObj.hasParameter)
				method.Invoke(this, new[] { methodObj.parameter.value });
			else
				method.Invoke(this, null);
		}
		#endregion

		public void MakeChoice(int index)
		{
			Debug.Log(index);
		}
	}
}
