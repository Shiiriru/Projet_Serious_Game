using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Other/Delay")]
	[NodeWidth(200)]
	[NodeTint("#ffcc00")]
	public class DelayNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Delay";
			base.Init();
		}

		public float duration;
	}
}