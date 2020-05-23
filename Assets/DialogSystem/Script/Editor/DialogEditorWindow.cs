using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogSystem
{
	public class DialogEditorWindow : EditorWindow
	{
		public static DialogEditorWindow instance { get; private set; }

		DialogListManager manager;
		//List<DialogGroupItem> dialogList = new List<DialogGroupItem>();
		DialogGroupItem selectedDialogGroup;
		Dictionary<DialogGroupItem, Editor> dialogEditorMap = new Dictionary<DialogGroupItem, Editor>();

		Vector2 scrollPosL;
		Vector2 scrollPosR;

		float leftSize = 150;
		bool resize = false;
		Rect cursorChangeRect;

		private void OnEnable()
		{
			instance = this;
			cursorChangeRect = new Rect(leftSize, 0, 5f, position.height);
		}

		private void OnDestroy()
		{
			instance = null;
		}

		// Add menu named "My Window" to the Window menu
		//[MenuItem("dialog/Editor")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
			DialogEditorWindow window = (DialogEditorWindow)GetWindow(typeof(DialogEditorWindow));
			window.Show();
		}

		public void SetTarget(DialogListManager _manager)
		{
			manager = _manager;
			selectedDialogGroup = manager.dialogList[0];
		}

		void OnGUI()
		{
			if(manager == null)
			{
				GUI.color = Color.yellow;
				GUILayout.BeginVertical("Box");
				GUILayout.Label("Current Dialog list is null\n Please refresh the editor");
				GUILayout.EndVertical();
				return;
			}

			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical("Box", GUILayout.Width(leftSize));
			scrollPosL = GUILayout.BeginScrollView(scrollPosL);

			foreach (var g in manager.dialogList)
			{
				if (g != null)
				{
					EditorGUI.BeginDisabledGroup(selectedDialogGroup == g);

					if (GUILayout.Button(g.name))
						selectedDialogGroup = g;

					EditorGUI.EndDisabledGroup();
				}
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();

			ResizeScrollView();

			GUILayout.BeginVertical("Box", GUILayout.MinWidth(200), GUILayout.MaxWidth(position.width));
			scrollPosR = GUILayout.BeginScrollView(scrollPosR);

			var currentEditor = GetCurrentDialogEditor();
			if (currentEditor != null)
				currentEditor.OnInspectorGUI();

			GUILayout.EndScrollView();
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			Repaint();
		}

		private void ResizeScrollView()
		{
			//GUI.DrawTexture(cursorChangeRect, EditorGUIUtility.whiteTexture);
			EditorGUIUtility.AddCursorRect(cursorChangeRect, MouseCursor.ResizeVertical);

			if (Event.current.type == EventType.MouseDown && cursorChangeRect.Contains(Event.current.mousePosition))
			{
				resize = true;
			}
			if (resize && Event.current.mousePosition.x > 100 && Event.current.mousePosition.x <= 200)
			{
				leftSize = Event.current.mousePosition.x;
				cursorChangeRect.Set(leftSize, cursorChangeRect.y, cursorChangeRect.width, cursorChangeRect.height);
			}
			if (Event.current.type == EventType.MouseUp)
				resize = false;
		}

		private Editor GetCurrentDialogEditor()
		{
			if (selectedDialogGroup == null)
				return null;

			Editor editor;

			if (!dialogEditorMap.TryGetValue(selectedDialogGroup, out editor))
			{
				dialogEditorMap[selectedDialogGroup] = editor = Editor.CreateEditor(selectedDialogGroup);
			}

			Editor.CreateCachedEditor(selectedDialogGroup, null, ref editor);
			editor.serializedObject.UpdateIfRequiredOrScript();

			return editor;
		}
	}

	//void OnGUIBranche(BrancheItem branche)
	//{

	//	//branche.text = EditorGUILayout.TextField("Text", branche.text);
	//	//branche.onSelectFunction = EditorGUILayout. ( branche.onSelectFunction, typeof(UnityEvent));

	//	//OnGUIActiveCondition(branche.activeConditions);
	//}

	//public class DialogGroupEditor
	//{
	//	DialogGroupItem groupItem;
	//	bool showConditions;

	//	public DialogGroupEditor(DialogGroupItem _item)
	//	{
	//		groupItem = _item;
	//	}

	//	public void OnGUIDialogGroup()
	//	{
	//		groupItem.id = EditorGUILayout.TextField("Group Name", groupItem.id);
	//		OnGUIActiveCondition(groupItem.activeConditions);

	//		foreach (var d in groupItem.dialogList)
	//			OnGUIDialogItem(d);

	//		if (GUILayout.Button("Add dialog"))
	//		{
	//			groupItem.dialogList.Add(new DialogItem());
	//		}
	//	}

	//	void OnGUIActiveCondition(List<ActiveCondition> conditions)
	//	{
	//		//active conditions
	//		bool showConditions = false;
	//		showConditions = EditorGUILayout.Foldout(showConditions, "Active Conditions");
	//		if (showConditions)
	//		{
	//			foreach (var c in conditions)
	//			{
	//				c.key = EditorGUILayout.TextField("Key", c.key);
	//				c.value = EditorGUILayout.TextField("Value", c.value);
	//			}
	//			if (GUILayout.Button("+"))
	//				conditions.Add(new ActiveCondition());
	//		}
	//	}

	//	void OnGUIDialogItem(DialogItem item)
	//	{
	//		item.characterName = EditorGUILayout.TextField("Character name", item.characterName);
	//		item.text = EditorGUILayout.TextArea("Text", item.text, GUILayout.MaxHeight(100));


	//		//foreach (var b in item.branches)
	//		//	OnGUIBranche(b);
	//	}
	//}
}