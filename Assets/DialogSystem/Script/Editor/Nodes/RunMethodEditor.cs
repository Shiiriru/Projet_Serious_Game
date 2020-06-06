using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(RunMethodNode))]
	public class RunMethodEditor : VariableObjectEditorBase
	{
		RunMethodNode node;
		DialogGraph graph;

		public override void OnCreate()
		{
			node = target as RunMethodNode;
			graph = (target as XNode.Node).graph as DialogGraph;

			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			DrawPorts();

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

				GetValueListWithMethods(graph, out vTypeList, out valNames, out valFullNames);
				DrawVariablMethodItemField(node.variableMethod, vTypeList, valNames, valFullNames);
			}

			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
		}
	}
}