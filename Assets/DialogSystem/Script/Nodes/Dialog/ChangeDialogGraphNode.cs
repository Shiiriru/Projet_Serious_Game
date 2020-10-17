using UnityEngine;
using UnityEditor;
using static XNode.Node;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Dialog/Change DialogGraph")]
	[NodeTint("#99ffcc")]
	public class ChangeDialogGraphNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Change DialogGraph";
			base.Init();
		}

		public DialogGraph dialogGraph;
	}
}