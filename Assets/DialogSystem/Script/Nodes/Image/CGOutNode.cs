using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Image/CG out")]
	[NodeTint("#00ccff")]
	public class CGOutNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "CG out";
			base.Init();
		}
	}
}