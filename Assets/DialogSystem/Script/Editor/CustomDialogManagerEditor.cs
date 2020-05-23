using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DialogSystem
{
	[CustomEditor(typeof(DialogListManager))]
	public class CustomDialogManagerEditor : Editor
	{
		DialogListManager manager;
		private void OnEnable()
		{
			manager = target as DialogListManager;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Add Group +"))
				AddNewGroup();

			GUILayout.Space(5);
			if (GUILayout.Button("Open Editor"))
				ShowWindow();
		}

		void AddNewGroup()
		{
			var newGroup = manager.AddGroup();
			if (string.IsNullOrEmpty(newGroup.name))
			{
				// Automatically remove redundant 'Node' postfix
				string typeName = typeof(DialogGroupItem).Name;
				typeName += manager.dialogList.Count - 1;
				newGroup.name = typeName;
			}

			AssetDatabase.CreateAsset(newGroup, $"Assets/{newGroup.name}.asset");
			AssetDatabase.SaveAssets();
			EditorGUIUtility.PingObject(newGroup);
		}

		void ShowWindow()
		{
			if (DialogEditorWindow.instance != null)
				DialogEditorWindow.instance.SetTarget(manager);
			else
			{
				DialogEditorWindow inst = ScriptableObject.CreateInstance<DialogEditorWindow>();
				// show the window
				inst.Show();
				inst.SetTarget(manager);
			}
		}
	}
}