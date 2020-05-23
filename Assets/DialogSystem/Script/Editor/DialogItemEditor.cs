using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DialogSystem
{
	[CustomEditor(typeof(DialogItem))]
	public class DialogItemEditor : Editor
	{
		DialogItem item;

		bool isShow = true;

		SerializedProperty bracheProperty;
		bool showBranches;

		GUIStyle brancheBoxStyle;
		const string methodPattern = @"(?<name>\w+)\(((?<vType>System.(Int32|Single|Boolean|String)) (?<vName>\w+))?\)";

		private void OnEnable()
		{
			item = target as DialogItem;
			if (item != null)
			{
				bracheProperty = serializedObject.FindProperty("branches");
				showBranches = item.branches.Count > 0;
			}
		}

		void InitStyles()
		{
			if (brancheBoxStyle == null)
			{
				brancheBoxStyle = new GUIStyle(GUI.skin.box);
				brancheBoxStyle.normal.background = MakeBoxTexture(1, 1, new Color(1f, 1f, 0.7f, 0.1f));
				brancheBoxStyle.normal.textColor = Color.clear;
			}
		}

		private Texture2D MakeBoxTexture(int width, int height, Color col)
		{
			Color[] pix = new Color[width * height];
			for (int i = 0; i < pix.Length; ++i)
			{
				pix[i] = col;
			}
			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();
			return result;
		}

		public override void OnInspectorGUI()
		{
			if (item == null)
				return;

			InitStyles();

			GUILayout.BeginVertical("Box");

			GUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Name"), GUIContent.none);
			GUILayout.Space(5);

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("x", GUILayout.Width(25)))
				RemoveDialogItem();
			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();

			isShow = EditorGUILayout.Foldout(isShow, "");
			if (isShow)
			{
				GUILayout.Space(5);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("characterName"));
				GUILayout.Label("Text");

				EditorStyles.textArea.wordWrap = true;
				item.text = EditorGUILayout.TextArea(item.text, EditorStyles.textArea, GUILayout.Height(50));

				OnGUIBranches();
			}

			GUILayout.Space(5);
			GUILayout.EndVertical();
			GUILayout.Space(5);

			serializedObject.ApplyModifiedProperties();
		}

		void OnGUIBranches()
		{
			GUILayout.Space(5);
			showBranches = EditorGUILayout.Foldout(showBranches, $"Branches ({item.branches.Count})");
			if (!showBranches)
				return;

			EditorGUILayout.PropertyField(bracheProperty.FindPropertyRelative("Array.size"));
			GUILayout.Space(5);

			foreach (var b in item.branches)
			{
				GUILayout.BeginVertical("box", brancheBoxStyle);

				b.text = EditorGUILayout.TextField("Text", b.text);

				if (item.targetGroup.variableSource == null)
				{
					GUI.color = Color.yellow;
					GUILayout.Label("No condition object :/");
					GUI.color = Color.white;

				}
				else
				{
					//condition value list
					List<FieldInfo> infoList = item.targetGroup.variableSource.GetSourceFieldInfos().ToList();
					var strList = infoList.Select(v => v.Name).ToList();
					List<VariableType> vTypeList = infoList.Select(i => i.FieldType.ToVariableType()).ToList();

					vTypeList.Insert(0, VariableType.Null);
					strList.Insert(0, "None");

					GUILayout.Space(5);
					OnGUIBranchOnSelectValue(b, vTypeList, strList);
					GUILayout.Space(5);

					OnGUIEditActiveConditions(b, vTypeList, strList);
				}

				GUILayout.EndVertical();
			}
		}

		void OnGUIBranchOnSelectValue(BrancheItem branche, List<VariableType> vTypeList, List<string> strList)
		{
			GUILayout.BeginVertical("box");
			GUILayout.Label("OnSelect");

			var vTypes2 = new List<VariableType>(vTypeList);
			var valFullNames = new List<string>(strList);
			var showStrs = new List<string>(strList);

			var methodList = item.targetGroup.variableSource.GetSourceMethods();
			foreach (var m in methodList)
			{
				try
				{
					var match = Regex.Match(m, methodPattern);
					if (match.Success)
					{
						vTypes2.Add(VariableType.Method);

						var type = match.Groups["vType"].Value;
						var name = match.Groups["name"].Value;
						if (type != null)
						{
							valFullNames.Add($"{name}({type} {match.Groups["vName"].Value})");
							showStrs.Add($"{name}({Type.GetType(type).ToShortTypeStr()} {match.Groups["vName"].Value})");
						}
						else
						{
							valFullNames.Add($"{name}()");
							showStrs.Add($"{name}()");
						}
					}
				}
				catch { }
			}

			EditorGUILayout.BeginHorizontal();
			//init selected index
			if (branche.onSelect.valueIndex < 0)
			{
				var type = branche.onSelect.vType;

				if (type == VariableType.Null)
					branche.onSelect.valueIndex = 0;
				else if (type == VariableType.Method)
				{
					if (valFullNames.Contains(branche.onSelect.method.fullMethod))
					{
						var index = valFullNames.IndexOf(branche.onSelect.method.fullMethod);
						if (branche.onSelect.valueIndex != index) branche.onSelect.valueIndex = index;
					}
				}
				else
				{
					if (valFullNames.Contains(branche.onSelect.variable.name))
					{
						var index = valFullNames.IndexOf(branche.onSelect.variable.name);
						if (branche.onSelect.valueIndex != index) branche.onSelect.valueIndex = index;
					}
				}
			}

			branche.onSelect.valueIndex = EditorGUILayout.Popup(branche.onSelect.valueIndex, showStrs.ToArray());
			branche.onSelect.vType = vTypes2[branche.onSelect.valueIndex];

			if (branche.onSelect.vType == VariableType.Method)
			{
				branche.onSelect.method.fullMethod = valFullNames[branche.onSelect.valueIndex];
				var match = Regex.Match(branche.onSelect.method.fullMethod, methodPattern);
				OnGUIMethod(branche.onSelect.method, match);
			}
			else
			{
				branche.onSelect.variable.name = branche.onSelect.valueIndex != 0 ? showStrs[branche.onSelect.valueIndex] : "";
				branche.onSelect.variable.vType = branche.onSelect.vType;
				OnGUIVariable(branche.onSelect.variable);
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}

		void OnGUIEditActiveConditions(BrancheItem branch, List<VariableType> vTypeList, List<string> strList)
		{
			GUILayout.BeginVertical("box");

			branch.showActiveCondition = EditorGUILayout.Foldout(branch.showActiveCondition, $"Active Conditions ({branch.activeConditions.Count})");
			if (branch.showActiveCondition)
			{
				if (branch.activeConditions.Count > 0)
				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Variable");
					EditorGUILayout.LabelField("Value");
					GUILayout.Space(20);
					EditorGUILayout.EndHorizontal();
				}

				foreach (var c in branch.activeConditions)
				{
					GUILayout.BeginHorizontal();
					//init selected index
					if (c.valueIndex < 0)
					{
						if (c.vType != VariableType.Null && strList.Contains(c.name))
						{
							var index = strList.IndexOf(c.name);
							if (c.valueIndex != index) c.valueIndex = index;
						}
						else
							c.valueIndex = 0;
					}
					c.valueIndex = EditorGUILayout.Popup(c.valueIndex, strList.ToArray());
					c.name = c.valueIndex != 0 ? strList[c.valueIndex] : "";
					c.vType = vTypeList[c.valueIndex];
					OnGUIVariable(c);

					if (GUILayout.Button("x", GUILayout.Width(20)))
						branch.activeConditions.Remove(c);

					GUILayout.EndHorizontal();
				}

				if (GUILayout.Button("New condition", GUILayout.Width(100)))
				{
					branch.activeConditions.Add(new VariableObject());
				}

			}
			GUILayout.EndVertical();
		}

		void OnGUIVariable(VariableBase c)
		{
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
				case VariableType.Method:

					break;
				default:
					GUILayout.FlexibleSpace();
					break;
			}
		}

		//method
		void OnGUIMethod(MethodObject method, Match match)
		{
			method.name = match.Groups["name"].Value;

			var vType = match.Groups["vType"].Value;
			method.hasParameter = !string.IsNullOrEmpty(vType);

			if (!string.IsNullOrEmpty(vType))
			{
				method.parameter.vType = Type.GetType(vType).ToVariableType();
				OnGUIVariable(method.parameter);
			}
			else
				GUILayout.FlexibleSpace();
		}

		void RemoveDialogItem()
		{
			item.targetGroup.RemoveDialogItem(item);
			DestroyImmediate(item, true);

			item = null;
			AssetDatabase.SaveAssets();
		}
	}
}