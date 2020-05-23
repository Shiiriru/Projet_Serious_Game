using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace DialogSystem.Demo
{
	public class TestVariableObject : VariableSourceObject
	{
		TestVariableSource source = new TestVariableSource();
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