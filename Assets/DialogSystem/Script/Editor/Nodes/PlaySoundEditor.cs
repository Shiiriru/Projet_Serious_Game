using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(PlaySoundNode))]
	public class PlaySoundEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("soundPath"), GUIContent.none);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("eventId"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}