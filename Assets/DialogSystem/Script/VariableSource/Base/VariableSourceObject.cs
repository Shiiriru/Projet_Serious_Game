using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DialogSystem
{
	public class VariableSourceObject : ScriptableObject
	{
		public virtual FieldInfo[] GetSourceFieldInfos() { throw new NotImplementedException(); }

		public virtual string[] GetSourceMethods() { throw new NotImplementedException(); }
	}
}