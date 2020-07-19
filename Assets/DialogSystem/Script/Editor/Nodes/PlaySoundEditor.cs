using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(PlaySoundNode))]
	public class PlaySoundEditor : DialogNodeEditorBase
	{
		PlaySoundNode node;
		public override void OnCreate()
		{
			node = target as PlaySoundNode;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("soundPath"), GUIContent.none);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("eventId"));
			if (!string.IsNullOrEmpty(node.eventId))
				EditorGUILayout.PropertyField(serializedObject.FindProperty("volume"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}