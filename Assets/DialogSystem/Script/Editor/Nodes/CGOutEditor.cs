using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(CGOutNode))]
	public class CGOutEditor : DialogNodeEditorBase
	{
		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();

			DrawDurationField();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}