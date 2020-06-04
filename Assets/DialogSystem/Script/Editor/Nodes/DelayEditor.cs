using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(DelayNode))]
	public class DelayEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			DrawDurationField();

			serializedObject.ApplyModifiedProperties();
		}
	}
}