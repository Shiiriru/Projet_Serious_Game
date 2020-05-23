using UnityEngine;
using UnityEditor;

namespace DialogSystem.Demo
{
	public class TestVariableSource : VariableSource, ITestConditionMethodSource
	{
		public int testInt;
		public float testFloat;
		public bool testBool;
		public string testString;

		public void OnSelectPlayChoice(int index) { }
	}

	public interface ITestConditionMethodSource
	{
		void OnSelectPlayChoice(int index);
	}
}