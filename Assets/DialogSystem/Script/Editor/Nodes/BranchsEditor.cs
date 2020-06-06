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
	public class BranchsEditor : VariableObjectEditorBase
	{
		DialogGraph graph;
		BranchsNode node;

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
					List<VariableType> vTypeList;
					List<string> valNames;
					List<string> valFullNames;

					GetValueListWithMethods(graph, out vTypeList,out valNames,out valFullNames);

					GUILayout.Space(5);
					GUILayout.BeginVertical("box");
					GUILayout.Label("OnSelect");
					DrawVariablMethodItemField(b.onSelect, vTypeList, valNames, valFullNames);
					GUILayout.EndVertical();
				}

				GUILayout.EndVertical();
			}

			EditorGUI.BeginDisabledGroup(!node.CanAddBranch());
			if (GUILayout.Button("+"))
				node.AddBranch();
			EditorGUI.EndDisabledGroup();
		}	
	}
}