using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;
using XNode;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using XNodeEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(BranchsNode))]
	public class BranchsEditor : DialogNodeEditorBase
	{
		DialogGraph graph;
		BranchsNode node;

		const string methodPattern = @"(?<name>\w+)\(((?<vType>System.(Int32|Single|Boolean|String)) (?<vName>\w+))?\)";

		public override void OnCreate()
		{
			node = target as BranchsNode;
			graph = (target as Node).graph as DialogGraph;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			if (node.branchs.Count == 0)
			{
				GUILayout.BeginHorizontal();
				NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
				NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
				GUILayout.EndHorizontal();
			}
			else
			{
				NodeEditorGUILayout.PortField(new GUIContent("input"), target.GetInputPort("input"));
			}

			OnGUIBranchs();

			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
		}

		void OnGUIBranchs()
		{
			foreach (var b in node.branchs)
			{
				var index = node.branchs.IndexOf(b);

				GUILayout.BeginVertical("box");

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();

				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("x", GUILayout.Width(25)))
					node.RemoveBranch(b);
				GUI.backgroundColor = Color.white;

				GUILayout.EndHorizontal();

				NodeEditorGUILayout.PortField(new GUIContent("condition"), target.GetInputPort($"bInput{index}"), GUILayout.MinWidth(0));
				GUILayout.Space(-18);
				NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort($"bOutput{index}"), GUILayout.MinWidth(0));				

				b.text = EditorGUILayout.TextField("Text", b.text);

				if (graph.variableSource == null)
				{
					GUI.color = Color.yellow;
					GUILayout.Label("No variable source :/");
					GUI.color = Color.white;
				}
				else
				{
					List<FieldInfo> infoList = graph.variableSource.GetSourceFieldInfos().ToList();

					var strList = infoList.Select(v => v.Name).ToList();
					var valFullNames = new List<string>(strList);
					List<VariableType> vTypeList = infoList.Select(i => i.FieldType.ToVariableType()).ToList();

					vTypeList.Insert(0, VariableType.Null);
					strList.Insert(0, "None");

					var methodList = graph.variableSource.GetSourceMethods();
					foreach (var m in methodList)
					{
						try
						{
							var match = Regex.Match(m, methodPattern);
							if (match.Success)
							{
								vTypeList.Add(VariableType.Method);

								var type = match.Groups["vType"].Value;
								var name = match.Groups["name"].Value;
								if (type != null)
								{
									valFullNames.Add($"{name}({type} {match.Groups["vName"].Value})");
									strList.Add($"{name}({Type.GetType(type).ToShortTypeStr()} {match.Groups["vName"].Value})");
								}
								else
								{
									valFullNames.Add($"{name}()");
									strList.Add($"{name}()");
								}
							}
						}
						catch { }
					}

					GUILayout.Space(5);
					OnGUIBranchOnSelectValue(b, vTypeList, strList, valFullNames);
				}
				GUILayout.EndVertical();
			}

			EditorGUI.BeginDisabledGroup(!node.CanAddBranch());
			if (GUILayout.Button("+"))
				node.AddBranch();
			EditorGUI.EndDisabledGroup();
		}

		void OnGUIBranchOnSelectValue(BrancheItem branche, List<VariableType> vTypeList, List<string> strList, List<string> valFullNames)
		{
			GUILayout.BeginVertical("box");
			GUILayout.Label("OnSelect");

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

			branche.onSelect.valueIndex = EditorGUILayout.Popup(branche.onSelect.valueIndex, strList.ToArray());
			branche.onSelect.vType = vTypeList[branche.onSelect.valueIndex];

			if (branche.onSelect.vType == VariableType.Method)
			{
				branche.onSelect.method.fullMethod = valFullNames[branche.onSelect.valueIndex];
				var match = Regex.Match(branche.onSelect.method.fullMethod, methodPattern);
				OnGUIMethod(branche.onSelect.method, match);
			}
			else
			{
				branche.onSelect.variable.name = branche.onSelect.valueIndex != 0 ? strList[branche.onSelect.valueIndex] : "";
				branche.onSelect.variable.vType = branche.onSelect.vType;
				OnGUIVariable(branche.onSelect.variable);
			}
			EditorGUILayout.EndHorizontal();

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
	}
}