using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DialogSystem
{
	[CustomEditor(typeof(DialogGroupItem))]
	public class DialogGroupEditor : Editor
	{
		DialogGroupItem groupItem;

		SerializedProperty activeConditionProperty;
		bool showVriableObject = true;

		SerializedProperty dialogListProperty;

		Dictionary<DialogItemBase, Editor> itemEditorMap = new Dictionary<DialogItemBase, Editor>();

		string variableSourcePath;
		Dictionary<string, System.Type> conditionObjList = new Dictionary<string, System.Type>();

		protected GUIStyle dialogItemStyle;
		void InitStyles()
		{
			if (dialogItemStyle == null)
			{
				dialogItemStyle = new GUIStyle(GUI.skin.box);
				dialogItemStyle.normal.background = EditorTools.MakeBoxTexture(1, 1, new Color(1, 1, 1, 0.15f));
				dialogItemStyle.normal.textColor = Color.clear;
			}
		}

		private void OnEnable()
		{
			groupItem = target as DialogGroupItem;
			activeConditionProperty = serializedObject.FindProperty("activeConditions");
			dialogListProperty = serializedObject.FindProperty("dialogList");

			variableSourcePath = Application.dataPath + @"\DialogSystem\Script\VariableSource\";
		}

		public override void OnInspectorGUI()
		{
			InitStyles();

			//EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
			if (GUILayout.Button("Add old dialogs"))
				groupItem.AddOldDialogsToList();

			GUILayout.Space(10);

			OnGuiActiveCondition();
			OnGUIDialogList();

			serializedObject.ApplyModifiedProperties();

			Repaint();
		}

		void OnGuiActiveCondition()
		{
			if (groupItem.variableSource == null)
			{
				ConditionsObjectPopup();
				GUILayout.Space(5);
				return;
			}

			GUILayout.BeginHorizontal();
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("variableSource"));
			EditorGUI.EndDisabledGroup();

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("x", GUILayout.Width(25)))
			{
				RemoveConditionsObject();
			}
			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();

			//OnGUIEditActiveConditions();

			GUILayout.Space(5);
		}

		void OnGUIEditActiveConditions()
		{
			if (groupItem.variableSource == null)
				return;

			GUILayout.BeginVertical("box");

			showVriableObject = EditorGUILayout.Foldout(showVriableObject, $"Variable Objects ({groupItem.activeConditions.Count})");
			if (showVriableObject)
			{
				var valueList = groupItem.variableSource.GetSourceFieldInfos().ToList();
				var strList = valueList.Select(v => v.Name).ToList();
				valueList.Insert(0, null);
				strList.Insert(0, "None");

				if (groupItem.activeConditions.Count > 0)
				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Variable");
					EditorGUILayout.LabelField("Value");
					GUILayout.Space(20);
					EditorGUILayout.EndHorizontal();
				}

				foreach (var c in groupItem.activeConditions)
				{
					EditorGUILayout.BeginHorizontal();
					//init selected index
					if (c.valueIndex < 0)
					{
						if (!string.IsNullOrEmpty(c.name) && strList.Contains(c.name))
						{
							var index = strList.IndexOf(c.name);
							if (c.valueIndex != index) c.valueIndex = index;
						}
						else
							c.valueIndex = 0;
					}

					c.valueIndex = EditorGUILayout.Popup(c.valueIndex, strList.ToArray());
					c.name = c.valueIndex != 0 ? strList[c.valueIndex] : "";
					c.vType = c.valueIndex != 0 ? valueList[c.valueIndex].FieldType.ToVariableType() : VariableType.Null;

					switch (c.vType)
					{
						case VariableType.Int:
							if (c.value == null)
								c.valueStr = "0";
							c.valueStr = EditorGUILayout.IntField(int.Parse(c.valueStr)).ToString();
							break;
						case VariableType.Float:
							if (c.value == null)
								c.valueStr = "0";
							c.valueStr = EditorGUILayout.FloatField(float.Parse(c.valueStr)).ToString();
							break;
						case VariableType.Bool:
							if (c.value == null)
								c.valueStr = "false";
							c.valueStr = EditorGUILayout.Toggle(bool.Parse(c.valueStr)).ToString();
							break;
						case VariableType.String:
							c.valueStr = EditorGUILayout.TextField(c.valueStr);
							break;
						default:
							GUILayout.FlexibleSpace();
							break;
					}

					if (GUILayout.Button("x", GUILayout.Width(20)))
						groupItem.activeConditions.Remove(c);

					EditorGUILayout.EndHorizontal();
				}

				//EditorGUILayout.PropertyField(activeConditionProperty.FindPropertyRelative("Array.size"));

				//if (activeConditionProperty.arraySize > 0)
				//{
				//	for (int i = 0; i < activeConditionProperty.arraySize; i++)
				//		EditorGUILayout.PropertyField(activeConditionProperty.GetArrayElementAtIndex(i), GUIContent.none);
				//}

				if (GUILayout.Button("New condition", GUILayout.Width(100)))
				{
					groupItem.activeConditions.Add(new VariableObject());
				}
			}

			GUILayout.EndVertical();
		}

		void OnGUIDialogList()
		{
			EditorGUILayout.LabelField("Dialogs", EditorStyles.boldLabel);

			try
			{
				foreach (var item in groupItem.itemList)
				{
					if (item.index.IsOdd())
						GUILayout.BeginVertical("Box", dialogItemStyle);
					else
						GUILayout.BeginVertical("Box");

					OnItemHeaderGUI(item);

					GetDialogItemEditor(item).OnInspectorGUI();

					GUILayout.Space(5);
					GUILayout.EndVertical();
					GUILayout.Space(5);
				}

			}
			catch { }

			if (GUILayout.Button("+"))
				ShowCreateItemMenu();
		}

		private void OnItemHeaderGUI(DialogItemBase item)
		{
			GUILayout.BeginVertical();

			GUILayout.Label($"{item.index}.  {item.name}");

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("^", GUILayout.Width(25)))
				groupItem.SwapItems(item.index, item.index - 1);

			if (GUILayout.Button("v", GUILayout.Width(25)))
				groupItem.SwapItems(item.index, item.index + 1);

			GUILayout.FlexibleSpace();

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("x", GUILayout.Width(25)))
			{
				groupItem.RemoveItem(item);
				DestroyImmediate(item, true);

				AssetDatabase.SaveAssets();
			}
			GUI.backgroundColor = Color.white;

			GUILayout.EndHorizontal();
			GUILayout.EndVertical();

			Event current = Event.current;
			if (GUILayoutUtility.GetLastRect().Contains(current.mousePosition) && current.type == EventType.ContextClick)
				ShowCreateItemMenu(item.index);

			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		}

		private Editor GetDialogItemEditor(DialogItemBase item)
		{
			Editor editor;

			if (!itemEditorMap.TryGetValue(item, out editor))
				itemEditorMap[item] = editor = Editor.CreateEditor(item);

			Editor.CreateCachedEditor(item, null, ref editor);
			editor.serializedObject.UpdateIfRequiredOrScript();

			return editor;
		}

		void ShowCreateItemMenu(int insertIndex = -1)
		{
			Event currentEvent = Event.current;

			Vector2 mousePos = currentEvent.mousePosition;

			// Now create the menu, add items and show it
			GenericMenu menu = new GenericMenu();
			if (insertIndex >= 0)
			{
				menu.AddDisabledItem(new GUIContent("INSERT"));
				menu.AddSeparator("");
			}

			menu.AddItem(new GUIContent("dialog"), false, () => CreateNewItem(typeof(DialogItem), insertIndex));
			menu.AddItem(new GUIContent("CG"), false, () => CreateNewItem(typeof(CGItem), insertIndex));
			menu.AddItem(new GUIContent("CG out"), false, () => CreateNewItem(typeof(CGOutItem), insertIndex));
			menu.AddItem(new GUIContent("Delay"), false, () => CreateNewItem(typeof(DelayItem), insertIndex));
			menu.ShowAsContext();
			currentEvent.Use();
		}

		void CreateNewItem(Type type, int insertIndex)
		{
			var item = groupItem.CreateItem(type);
			if (item == null)
				return;

			if (insertIndex < 0)
				groupItem.AddItemToList(item);
			else
				groupItem.InsertItemToList(item, insertIndex);

			if (string.IsNullOrEmpty(item.name))
			{
				string typeName = type.Name.Replace("Item", "");
				item.name = ObjectNames.NicifyVariableName(typeName);
			}
			AssetDatabase.AddObjectToAsset(item, target);
			AssetDatabase.SaveAssets();

			Repaint();
			//return dialog;
		}

		Rect conditionObjButtonRect;

		void ConditionsObjectPopup()
		{
			if (GUILayout.Button("Add VariableSourceObject"))
			{
				string[] conditionObjs =
					Directory.GetFiles(variableSourcePath, "*.cs", SearchOption.TopDirectoryOnly);
				conditionObjList.Clear();

				foreach (string path in conditionObjs)
				{
					string assetPath = "Assets" + path.Replace(Application.dataPath, "").Replace('\\', '/');
					MonoScript obj = (MonoScript)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript));
					var objType = obj.GetClass();

					if (System.Activator.CreateInstance(objType) is VariableSourceObject)
						conditionObjList.Add(objType.Name, objType);
				}

				var popup = new ConditionsObjectSelectorPopup();
				popup.Init(groupItem, conditionObjList);

				PopupWindow.Show(conditionObjButtonRect, popup);
			}

			if (Event.current.type == EventType.Repaint)
				conditionObjButtonRect = GUILayoutUtility.GetLastRect();
		}

		void RemoveConditionsObject()
		{
			var obj = groupItem.variableSource;
			groupItem.RemoveConditionObject();
			DestroyImmediate(obj, true);

			AssetDatabase.SaveAssets();
		}
	}
}