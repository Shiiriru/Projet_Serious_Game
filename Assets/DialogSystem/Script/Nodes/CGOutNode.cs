using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("CG out")]
	[NodeWidth(200)]
	[NodeTint("#00ccff")]
	public class CGOutNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "CG out";
			base.Init();
		}

		public float duration;
		public bool isWait;
	}
}