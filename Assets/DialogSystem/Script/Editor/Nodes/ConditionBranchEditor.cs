using UnityEngine;
using UnityEditor;
using System.Linq;
using XNodeEditor;
using System.Collections.Generic;
using System.Reflection;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ConditionBranchNode))]
	public class ConditionBranchEditor : VariableObjectEditorBase
	{
		ConditionBranchNode node;
		DialogGraph graph;

		int oldValueIndex;
		public override void OnCreate()
		{
			node = target as ConditionBranchNode;
			graph = (target as XNode.Node).graph as DialogGraph;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
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

			GUILayout.Space(-20);
			OnGUIEditActiveConditions();

			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
		}

		void OnGUIEditActiveConditions()
		{
			if (graph.variableSource == null)
			{
				GUI.color = Color.yellow;
				GUILayout.Label("No variable source :/");
				GUI.color = Color.white;
				return;
			}

			var vTypeList = new List<VariableType>();
			var strList = new List<string>();
			GetValueList(graph, out vTypeList, out strList);

			var condition = node.condition;
			//init selected index
			if (condition.valueIndex < 0)
			{
				if (!string.IsNullOrEmpty(condition.name) && strList.Contains(condition.name))
				{
					var index = strList.IndexOf(condition.name);
					if (condition.valueIndex != index)
						oldValueIndex = condition.valueIndex = index;
				}
				else
					oldValueIndex = condition.valueIndex = 0;
			}

			condition.valueIndex = EditorGUILayout.Popup(condition.valueIndex, strList.ToArray());
			condition.name = condition.valueIndex != 0 ? strList[condition.valueIndex] : "";
			condition.vType = vTypeList[condition.valueIndex];

			//change condition
			if (oldValueIndex != condition.valueIndex)
			{
				if (condition.valueIndex != 0)
				{
					if (condition.vType == VariableType.Bool && (node.branchs.Count < 1 || node.branchs[0].ToValue(condition.vType) == null))
					{
						node.branchs.Clear();
						node.branchs.Add("True");
						node.branchs.Add("False");
					}
				}
				else
					node.branchs.Clear();
				oldValueIndex = condition.valueIndex;
			}

			for (int i = 0; i < node.branchs.Count; i++)
				DrawBranchField(node.branchs[i], i);

			if (condition.valueIndex != 0 && node.CanAddBranch() && node.condition.vType != VariableType.Bool)
				if (GUILayout.Button("New Branch"))
					node.AddBranch();
		}

		void DrawBranchField(string branch, int index)
		{
			var vType = node.condition.vType;

			GUILayout.BeginHorizontal();

			switch (vType)
			{
				case VariableType.Int:
					if (branch.ToValue(vType) == null)
						branch = "0";
					branch = EditorGUILayout.IntField(int.Parse(branch), GUILayout.Width(180)).ToString();
					break;
				case VariableType.Float:
					if (branch.ToValue(vType) == null)
						branch = "0";
					branch = EditorGUILayout.FloatField(float.Parse(branch), GUILayout.Width(180)).ToString();
					break;
				case VariableType.Bool:
					EditorGUILayout.LabelField(branch, GUILayout.Width(200));
					break;
				case VariableType.String:
					branch = EditorGUILayout.TextField(branch, GUILayout.Width(180));
					break;
			}

			if (vType != VariableType.Bool)
			{
				if (GUILayout.Button("x", GUILayout.Width(20)))
					node.RemoveBranch(index);
			}

			NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort($"bOutput{index}"), GUILayout.MinWidth(0));

			GUILayout.EndHorizontal();
		}
	}
}
