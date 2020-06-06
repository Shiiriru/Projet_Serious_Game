using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Condition/Condition Branch")]
	[NodeTint("#ff5050")]
	public class ConditionBranchNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Condition Branch";
			base.Init();
		}

		public VariableObject condition = new VariableObject();
		public List<string> branchs = new List<string>();

		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput0;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput1;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput2;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput3;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput4;

		public void AddBranch()
		{
			branchs.Add("");
		}

		public void RemoveBranch(int index)
		{
			branchs.RemoveAt(index);
		}

		//remove last answer if out of range
		public bool CanAddBranch()
		{
			return branchs.Count < 5;
		}

		public NodePort GetOutputConnection(int index)
		{
			NodePort port = null;

			if (branchs.Count <= index) return null;
			port = GetOutputPort("bOutput" + index);

			if (port == null) return null;
			return port.Connection;
		}
	}
}



