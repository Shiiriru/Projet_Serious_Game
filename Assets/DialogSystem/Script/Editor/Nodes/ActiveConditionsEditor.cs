using UnityEngine;
using UnityEditor;
using System.Linq;
using XNodeEditor;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ActiveConditionsNode))]
	public class ActiveConditionsEditor : DialogNodeEditorBase
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

			var valueList = graph.variableSource.GetSourceFieldInfos().ToList();
			var strList = valueList.Select(v => v.Name).ToList();
			valueList.Insert(0, null);
			strList.Insert(0, "None");

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
