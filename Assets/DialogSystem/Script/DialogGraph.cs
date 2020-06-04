using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DialogSystem
{
	[CreateAssetMenu(fileName = "NewDialogGraph", menuName = "Dialog/New Dialog Graph")]
	public class DialogGraph : NodeGraph
	{
		public VariableSourceObject variableSource;

		public VariableSourceObject AddVariableSource(Type type)
		{
			VariableSourceObject obj = CreateInstance(type) as VariableSourceObject;
			variableSource = obj;
			return obj;
		}

		public void RemoveVariableSource()
		{
			if (Application.isPlaying) Destroy(variableSource);
			variableSource = null;
		}
	}
}