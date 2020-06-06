using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(DurationNodeBase))]
	public class DurationNodeEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();

			DrawDurationField();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}