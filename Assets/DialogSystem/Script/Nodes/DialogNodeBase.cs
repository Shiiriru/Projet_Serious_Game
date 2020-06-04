using UnityEngine;
using UnityEditor;
using XNode;

namespace DialogSystem.Nodes
{
	[NodeWidth(250)]
	[NodeTint("#CCCCFF")]
	public abstract class DialogNodeBase : Node
	{
		[Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public DialogNodeBase input;
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase output;
	}
}