using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(DialogNode))]
	public class DialogNodeEditor : DialogNodeEditorBase
	{
		DialogNode node;
		public override void OnCreate()
		{
			node = target as DialogNode;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("characterName"), new GUIContent("name"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("displaySide"));

			node.showAdvance = EditorGUILayout.Foldout(node.showAdvance, "Advance");
			if (node.showAdvance)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty("disableScene"));

				EditorGUILayout.PropertyField(serializedObject.FindProperty("displayAll"));
				if (!node.displayAll)
				{
					EditorGUILayout.PropertyField(serializedObject.FindProperty("displaySpeed"));
					DrawDurationField();
				}
			}

			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
		}
	}
}