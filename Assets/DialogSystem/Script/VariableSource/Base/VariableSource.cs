﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DialogSystem
{
	public class VariableSource
	{
		public virtual FieldInfo[] GetFieldInfos()
		{
			var fieldValues =
				this.GetType().GetFields().ToArray();

			return fieldValues;
		}

		public string[] GetMethods()
		{
			List<string> methodList = new List<string>();
			foreach (var method in this.GetType().GetMethods())
			{
				var parametre = method.GetParameters().Select(x => x.ParameterType + " " + x.Name).FirstOrDefault();
				methodList.Add($"{method.Name}({parametre})");
			}
			return methodList.ToArray();
		}

		public object GetValue(string name)
		{
			var variable = this.GetType().GetFields().Where(f => f.Name == name).FirstOrDefault();
			return variable != null ? variable.GetValue(this) : null;
		}

		public object GetValue(VariableObject obj)
		{
			var variable = this.GetType().GetFields().Where(f => f.Name == obj.name && f.FieldType == obj.vType.ToType()).FirstOrDefault();
			return variable != null ? variable.GetValue(this) : null;
		}

		//public void SetValue(object target, string value)
		//{
		//	var variable = GetValues().Where(v => v.GetType() == target.GetType() && nameof(v) == nameof(target)).FirstOrDefault();
		//	if (variable == null)
		//		return;

		//	SetVal(variable, value);
		//}

		public void SetValue(string name, object value)
		{
			var fieldInfo = GetType().GetFields().Where(f => f.Name == name).FirstOrDefault();
			if (fieldInfo == null)
			{
				Debug.LogError($"Variable '{name}' isn't implement in {GetType().Name}");
				return;
			}

			try
			{
				fieldInfo.SetValue(this, value.ToString().ToValue(fieldInfo.FieldType));
			}
			catch
			{
				Debug.LogError($"Variable '{name}' can't equals to {value}");
			}
		}

	}
}