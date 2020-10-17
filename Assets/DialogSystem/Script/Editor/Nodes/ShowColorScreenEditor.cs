using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ShowColorScreenNode))]
	public class ShowColorScreenEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("color"));
			DrawDurationField();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("resetColor"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait")); 

			serializedObject.ApplyModifiedProperties();
		}
	}
}