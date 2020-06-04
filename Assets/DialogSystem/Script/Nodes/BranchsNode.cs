using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using XNode;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Branchs")]
	[NodeWidth(300)]
	[NodeTint("#99ffcc")]
	public class BranchsNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Branchs";
			base.Init();
		}

		public List<BrancheItem> branchs = new List<BrancheItem>();

		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public ActiveConditionsNode bInput0;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput0;

		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public ActiveConditionsNode bInput1;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput1;

		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public ActiveConditionsNode bInput2;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput2;

		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public ActiveConditionsNode bInput3;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput3;

		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public ActiveConditionsNode bInput4;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase bOutput4;


		public void AddBranch()
		{
			branchs.Add(new BrancheItem());
		}

		public void RemoveBranch(BrancheItem item)
		{
			branchs.Remove(item);
		}

		//remove last answer if out of range
		public bool CanAddBranch()
		{
			return branchs.Count < 5;
		}

		public NodePort GetActiveConditions(int index)
		{
			NodePort port = null;

			if (branchs.Count <= index) return null;
			port = GetInputPort("bInput" + index);

			if (port == null) return null;
			return port.Connection;
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