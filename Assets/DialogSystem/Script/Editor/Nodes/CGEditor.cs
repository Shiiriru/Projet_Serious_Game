using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(CGNode))]
	public class CGEditor : DialogNodeEditorBase
	{
		CGNode node;
		public override void OnCreate()
		{
			node = target as CGNode;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			DrawPorts();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"));

			var imgPriveiw = node.sprite;
			if (imgPriveiw != null)
				GUILayout.Label(imgPriveiw.texture, GUILayout.Width(200), GUILayout.Height(113));

			DrawDurationField();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}