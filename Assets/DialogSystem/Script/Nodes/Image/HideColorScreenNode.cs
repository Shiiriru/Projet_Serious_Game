using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/Color Screen/Hide")]
	[NodeTint("#00ccff")]
	public class HideColorScreenNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Hide Color Screen";
			base.Init();
		}
	}
}