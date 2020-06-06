using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem
{


	[Serializable]
	public class BrancheItem
	{
		public string text;
		public VariableMethodItem onSelect = new VariableMethodItem();
	}
}