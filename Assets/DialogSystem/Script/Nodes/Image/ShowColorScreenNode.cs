using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/Color Screen/Show")]
	[NodeWidth(250)]
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
		//reset color to the same color base as changing color
		public bool resetColor = true;
	}
}