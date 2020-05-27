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

		Dictionary<DialogItem, Editor> dialogItemEditorMap = new Dictionary<DialogItem, Editor>();

		string variableSourcePath;
		Dictionary<string, System.Type> conditionObjList = new Dictionary<string, System.Type>();

		private void OnEnable()
		{
			groupItem = target as DialogGroupItem;
			activeConditionProperty = serializedObject.FindProperty("activeConditions");
			dialogListProperty = serializedObject.FindProperty("dialogList");

			variableSourcePath = Application.dataPath + @"\DialogSystem\Script\VariableSource\";
		}

		public override void OnInspectorGUI()
		{
			//base.OnInspectorGUI();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));

			OnGuiActiveCondition();
			OnGUIDialogList();
			//EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogList"), true);

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

			OnGUIEditActiveConditions();

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
				foreach (var dialog in groupItem.dialogList)
					GetDialogItemEditor(dialog).OnInspectorGUI();
			}
			catch { }

			if (GUILayout.Button("+"))
				CreateDialogItem();
		}

		private Editor GetDialogItemEditor(DialogItem item)
		{
			Editor editor;

			if (!dialogItemEditorMap.TryGetValue(item, out editor))
				dialogItemEditorMap[item] = editor = Editor.CreateEditor(item);

			Editor.CreateCachedEditor(item, null, ref editor);
			editor.serializedObject.UpdateIfRequiredOrScript();

			return editor;
		}

		void CreateDialogItem()
		{
			var dialog = groupItem.AddDialogItem();

			if (string.IsNullOrEmpty(dialog.name))
			{
				string typeName = typeof(DialogItem).Name + (groupItem.dialogList.Count - 1);
				dialog.name = ObjectNames.NicifyVariableName(typeName);
			}
			AssetDatabase.AddObjectToAsset(dialog, target);
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