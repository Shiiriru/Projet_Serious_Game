using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/Screen Flash")]
	[NodeTint("#00ccff")]
	public class ScreenFlashNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Flash";
			base.Init();
		}

		public Color color = Color.white;
	}
}