using UnityEngine;
using UnityEditor;
using System.Linq;
using XNodeEditor;
using System.Collections.Generic;
using System.Reflection;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ActiveConditionsNode))]
	public class ActiveConditionsEditor : VariableObjectEditorBase
	{
		ActiveConditionsNode node;
		DialogGraph graph;

		public override void OnCreate()
		{
			node = target as ActiveConditionsNode;
			graph = (target as XNode.Node).graph as DialogGraph;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
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

			if (node.conditions.Count > 0)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Variable", GUILayout.Width(110));
				EditorGUILayout.LabelField("Value");
				EditorGUILayout.EndHorizontal();
			}

			foreach (var c in node.conditions)
			{
				EditorGUILayout.BeginHorizontal("box");

				DrawVariableObjectField(c, vTypeList, strList);

				if (GUILayout.Button("x", GUILayout.Width(20)))
					node.conditions.Remove(c);

				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button("New condition"))
			{
				node.conditions.Add(new VariableObject());
			}
		}
	}
}
