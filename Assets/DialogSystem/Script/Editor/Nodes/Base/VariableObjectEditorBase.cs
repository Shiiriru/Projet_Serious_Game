using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace DialogSystem.Nodes
{
	public class VariableObjectEditorBase : DialogNodeEditorBase
	{
		const string methodPattern = @"(?<name>\w+)\(((?<vType>System.(Int32|Single|Boolean|String)) (?<vName>\w+))?\)";

		public void GetValueList(DialogGraph graph, out List<VariableType> vTypeList, out List<string> valNames)
		{
			var infoList = graph.variableSource.GetSourceFieldInfos().ToList();

			vTypeList = infoList.Select(i => i.FieldType.ToVariableType()).ToList();
			valNames = infoList.Select(v => v.Name).ToList();

			vTypeList.Insert(0, VariableType.Null);
			valNames.Insert(0, "None");
		}

		public void GetValueListWithMethods(DialogGraph graph, out List<VariableType> vTypeList, out List<string> valNames, out List<string> valFullNames)
		{
			GetValueList(graph, out vTypeList, out valNames);

			valFullNames = new List<string>(valNames);

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
							valNames.Add($"{name}({Type.GetType(type).ToShortTypeStr()} {match.Groups["vName"].Value})");
							valFullNames.Add($"{name}({type} {match.Groups["vName"].Value})");
						}
						else
						{
							valFullNames.Add($"{name}()");
							valNames.Add($"{name}()");
						}
					}
				}
				catch { }
			}
		}

		public void DrawVariableObjectField(VariableObject variableobj, List<VariableType> vTypeList, List<string> strList)
		{
			EditorGUILayout.BeginHorizontal();
			//init selected index
			if (variableobj.valueIndex < 0)
			{
				if (!string.IsNullOrEmpty(variableobj.name) && strList.Contains(variableobj.name))
				{
					var index = strList.IndexOf(variableobj.name);
					if (variableobj.valueIndex != index) variableobj.valueIndex = index;
				}
				else
					variableobj.valueIndex = 0;
			}

			variableobj.valueIndex = EditorGUILayout.Popup(variableobj.valueIndex, strList.ToArray());
			variableobj.name = variableobj.valueIndex != 0 ? strList[variableobj.valueIndex] : "";
			variableobj.vType = vTypeList[variableobj.valueIndex];

			OnGUIVariable(variableobj);
			EditorGUILayout.EndHorizontal();
		}

		public void DrawVariablMethodItemField(VariableMethodItem item, List<VariableType> vTypeList, List<string> valNames, List<string> valFullNames)
		{
			EditorGUILayout.BeginHorizontal();

			//init selected index
			if (item.valueIndex < 0)
			{
				var type = item.vType;

				if (type == VariableType.Null)
					item.valueIndex = 0;
				else if (type == VariableType.Method)
				{
					if (valFullNames.Contains(item.method.fullMethod))
					{
						var index = valFullNames.IndexOf(item.method.fullMethod);
						if (item.valueIndex != index) item.valueIndex = index;
					}
				}
				else
				{
					if (valFullNames.Contains(item.variable.name))
					{
						var index = valFullNames.IndexOf(item.variable.name);
						if (item.valueIndex != index) item.valueIndex = index;
					}
				}
			}

			item.valueIndex = EditorGUILayout.Popup(item.valueIndex, valNames.ToArray());
			item.vType = vTypeList[item.valueIndex];

			if (item.vType == VariableType.Method)
			{
				item.method.fullMethod = valFullNames[item.valueIndex];
				var match = Regex.Match(item.method.fullMethod, methodPattern);

				OnGUIMethod(item.method, match);
			}
			else
			{
				item.variable.name = item.valueIndex != 0 ? valNames[item.valueIndex] : "";
				item.variable.vType = item.vType;
				OnGUIVariable(item.variable);
			}

			EditorGUILayout.EndHorizontal();
		}

		public void OnGUIVariable(VariableBase v)
		{
			switch (v.vType)
			{
				case VariableType.Int:
					if (v.value == null)
						v.valueStr = "0";
					v.valueStr = EditorGUILayout.IntField(int.Parse(v.valueStr)).ToString();
					break;
				case VariableType.Float:
					if (v.value == null)
						v.valueStr = "0";
					v.valueStr = EditorGUILayout.FloatField(float.Parse(v.valueStr)).ToString();
					break;
				case VariableType.Bool:
					if (v.value == null)
						v.valueStr = "false";
					v.valueStr = EditorGUILayout.Toggle(bool.Parse(v.valueStr)).ToString();
					break;
				case VariableType.String:
					v.valueStr = EditorGUILayout.TextField(v.valueStr);
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