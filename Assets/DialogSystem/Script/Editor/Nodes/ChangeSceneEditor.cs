using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ChangeSceneNode))]
	public class ChangeSceneEditor : DialogNodeEditorBase
	{
		ChangeSceneNode node;
		public override void OnCreate()
		{
			node = target as ChangeSceneNode;
			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("scene"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("doTransition"));
			if (node.doTransition)
			{
				DrawDurationField();
				EditorGUILayout.PropertyField(serializedObject.FindProperty("dateInfos"));
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}