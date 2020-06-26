using UnityEngine;
using UnityEditor;
using FMODUnity;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Sound/Stop Sound")]
	[NodeTint("#66ff33")]
	public class StopSoundNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Stop Sound";
			base.Init();
		}

		public string eventId;
		public bool fadeOut;
	}
}