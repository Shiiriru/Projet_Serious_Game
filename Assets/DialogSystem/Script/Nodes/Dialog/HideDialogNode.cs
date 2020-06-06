using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Dialog/Hide Dialog")]
	[NodeTint("#99ffcc")]
	public class HideDialogNode : DurationNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Hide Dialog";
			base.Init();
		}
	}
}