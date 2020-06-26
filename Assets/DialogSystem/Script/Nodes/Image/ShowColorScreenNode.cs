using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/Show Color Screen")]
	[NodeTint("#00ccff")]
	public class ShowColorScreenNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Show Color Screen";
			base.Init();
		}

		public Color color;
	}
}