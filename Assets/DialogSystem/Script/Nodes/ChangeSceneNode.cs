using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Change Scene")]
	[NodeWidth(300)]
	[NodeTint("#ffcc00")]
	public class ChangeSceneNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Change scene";
			base.Init();
		}

		public SceneAsset scene;
		public float duration;
		public DatePanelInfosObject dateInfos;
	}
}