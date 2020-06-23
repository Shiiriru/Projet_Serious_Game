using UnityEngine;
using UnityEditor;
using XNode;

namespace DialogSystem.Nodes
{
	[NodeWidth(300)]
	[NodeTint("#ffff99")]
	public class StartPoint : Node
	{
		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)] public DialogNodeBase output;

		protected override void Init()
		{
			name = "START";
			base.Init();
		}

		public bool showPlayerUIAfterDialog = true;
	}
}