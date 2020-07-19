using UnityEngine;
using UnityEditor;
using FMODUnity;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Sound/Play Sound")]
	[NodeTint("#66ff33")]
	[NodeWidth(300)]
	public class PlaySoundNode : DialogNodeBase
	{

		// Use this for initialization
		protected override void Init()
		{
			name = "Play Sound";
			base.Init();
		}

		[EventRef] public string soundPath;
		//uniquement for sound event
		public string eventId;
		[Range(0, 1)] public float volume = 1;
	}
}