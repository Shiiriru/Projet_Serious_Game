using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem
{
	[Serializable]
	public class OnSelectBracheValue
	{
		public VariableType vType;
		public VariableObject variable = new VariableObject();
		public MethodObject method = new MethodObject();

		[NonSerialized]
		public int valueIndex = -1;
	}

	[Serializable]
	public class BrancheItem
	{
		public string text;

		public OnSelectBracheValue onSelect = new OnSelectBracheValue();
		public List<VariableObject> activeConditions = new List<VariableObject>();

		public bool showActiveCondition;
	}
}