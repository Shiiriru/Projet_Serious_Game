using System;
using System.Reflection;
using UnityEngine;

namespace DialogSystem
{
	public class VariableSourceManager : MonoBehaviour
	{
		public virtual FieldInfo[] GetSourceFieldInfo()
		{
			throw new NotImplementedException();
		}

		public virtual object GetValue(string name)
		{
			throw new NotImplementedException();
		}
		public virtual object GetValue(VariableObject obj)
		{
			throw new NotImplementedException();
		}

		public virtual void SetValue(VariableObject variable) { }

		public virtual void SetValue(string variableName, object value) { }

		public virtual void PlayMethod(MethodObject methodObj) { }
	}
}