using System;
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

		public object GetValue(object obj)
		{
			var variable = this.GetType().GetFields().Where(f => f.Name == nameof(obj) && f.FieldType == obj.GetType()).FirstOrDefault();
			return variable != null ? variable.GetValue(this) : null;
		}

		//public void SetValue(object target, string value)
		//{
		//	var variable = GetValues().Where(v => v.GetType() == target.GetType() && nameof(v) == nameof(target)).FirstOrDefault();
		//	if (variable == null)
		//		return;

		//	SetVal(variable, value);
		//}

		public void SetValue(string name, string value)
		{
			var variable = GetFieldInfos().Where(f => f.Name == name).FirstOrDefault();
			if (variable == null)
				return;
			SetVal(variable, value);
		}

		void SetVal(object variable, string value)
		{
			Type t = variable.GetType();
			if (t == typeof(int))
			{
				var newVal = 0;
				if (int.TryParse(value, out newVal))
					variable = newVal;
			}
			else if (t == typeof(float))
			{
				var newVal = 0.0f;
				if (float.TryParse(value, out newVal))
					variable = newVal;
			}
			else if (t == typeof(bool))
			{
				var newVal = false;
				if (bool.TryParse(value, out newVal))
					variable = newVal;
			}
			else if (t == typeof(string))
			{
				variable = value;
			}
		}

	}
}