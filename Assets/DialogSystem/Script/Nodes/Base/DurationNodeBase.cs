using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[NodeWidth(200)]
	[NodeTint("#00ccff")]
	public class DurationNodeBase : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			base.Init();
		}

		public float duration;
		public bool isWait;
	}
}