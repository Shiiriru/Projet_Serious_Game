using UnityEngine;
using UnityEditor;
using XNode;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Other/Run Method")]
	[NodeTint("#ffcc00")]
	public class RunMethodNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Run Method/Change variable";
			base.Init();
		}

		public VariableMethodItem variableMethod = new VariableMethodItem();
	}
}