using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("CG/New CG")]
	[NodeTint("#00ccff")]
	public class CGNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "CG";
			base.Init();
		}

		public Sprite sprite;

		public float duration;
		public bool isWait;
	}
}