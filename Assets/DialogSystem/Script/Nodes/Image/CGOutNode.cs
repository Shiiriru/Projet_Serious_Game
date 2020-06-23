using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/Hide CG")]
	[NodeTint("#00ccff")]
	public class CGOutNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Hide CG";
			base.Init();
		}
	}
}