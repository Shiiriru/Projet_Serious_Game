using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(StopSoundNode))]
	public class StopSoundEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("eventId"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeOut"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}