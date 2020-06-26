using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ScreenFlashNode))]
	public class ScreenFlashEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("color"));
			DrawDurationField();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}