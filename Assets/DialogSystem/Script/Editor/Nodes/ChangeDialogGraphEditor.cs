using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ChangeDialogGraphNode))]
	public class ChangeDialogGraphEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogGraph"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}