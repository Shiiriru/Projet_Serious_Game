using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ChangeSceneNode))]
	public class ChangeSceneEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("scene"));

			DrawDurationField();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dateInfos"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}