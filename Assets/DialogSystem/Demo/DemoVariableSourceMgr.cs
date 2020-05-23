using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DialogSystem.Demo
{
	public class DemoVariableSourceMgr : VariableSourceManager, ITestConditionMethodSource
	{
		public TestVariableSource variableSource = new TestVariableSource();

		private void Start()
		{
			variableSource.testBool = true;
		}

		public override FieldInfo[] GetSourceFieldInfo()
		{
			return variableSource.GetFieldInfos();
		}

		public override object GetValue(string name)
		{
			return variableSource.GetValue(name);
		}

		public override object GetValue(object obj)
		{
			return variableSource.GetValue(obj);
		}

		public override void SetValue(VariableObject variable)
		{
			variableSource.SetValue(variable.name, variable.valueStr);
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

		public void OnSelectPlayChoice(int index)
		{
			Debug.Log(index);
		}
	}
}
