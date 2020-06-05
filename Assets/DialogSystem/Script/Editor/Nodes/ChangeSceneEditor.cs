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
			EditorGUILayout.PropertyField(serializedObject.FindProperty("scene"), GUIContent.none);

			serializedObject.ApplyModifiedProperties();
		}
	}
}