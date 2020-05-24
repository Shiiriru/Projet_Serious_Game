using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DialogSystem
{
	//scriptable object
	public class VariableObjectSoldat : VariableSourceObject
	{
		SoldatVariableSource source = new SoldatVariableSource();

		//can only support int, float, bool, string
		public override FieldInfo[] GetSourceFieldInfos()
		{
			return source.GetFieldInfos();
		}

		public override string[] GetSourceMethods()
		{
			return source.GetMethods();
		}
	}
}