using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Other/Change Scene")]
	[NodeWidth(320)]
	[NodeTint("#ffcc00")]
	public class ChangeSceneNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Change scene";
			base.Init();
		}

		public SceneReference scene;
		public float duration;
		public DatePanelInfosObject dateInfos;
	}
}