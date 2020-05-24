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

		public virtual object GetValue(object obj)
		{
			throw new NotImplementedException();
		}

		public virtual void SetValue(VariableObject variable) { }

		public virtual void PlayMethod(MethodObject methodObj){}
	}
}