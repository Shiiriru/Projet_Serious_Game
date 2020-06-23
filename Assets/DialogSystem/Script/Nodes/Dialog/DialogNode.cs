using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Dialog/New Dialog")]
	[NodeWidth(350)]
	[NodeTint("#99ffcc")]
	public class DialogNode : DialogNodeBase
	{

		public enum DisplaySide
		{
			Right,
			Left
		}

		// Use this for initialization
		protected override void Init()
		{
			name = "Dialog";
			base.Init();
		}

		public DisplaySide displaySide;
		public string characterName;
		[TextArea] public string text;

		public bool showAdvance;

		public bool disableScene = true;
		public bool displayAll;
		[Range(1, 5)] public int displaySpeed = 3;
		public float duration = 0.2f;

	}
}