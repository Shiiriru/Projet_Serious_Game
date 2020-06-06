using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using XNode;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Condition/Active Condition")]
	[NodeTint("#ff5050")]
	public class ActiveConditionsNode : Node
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Active Condition";
			base.Init();
		}

		public List<VariableObject> conditions = new List<VariableObject>();
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)] public ActiveConditionsNode output;
	}
}