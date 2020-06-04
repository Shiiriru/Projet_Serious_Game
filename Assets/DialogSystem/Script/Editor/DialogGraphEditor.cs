using DialogSystem.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogSystem
{
	[CustomNodeGraphEditor(typeof(DialogGraph))]
	public class DialogGraphEditor : NodeGraphEditor
	{
		public override void OnOpen()
		{
			window.titleContent = new GUIContent(Selection.activeObject.name);

			var graph = target as DialogGraph;
			if (!graph.nodes.Any(n => n is StartPoint))
			{
				CreateNode(typeof(StartPoint), new Vector2(-250, -20));
			}

			base.OnOpen();
		}

		public override string GetNodeMenuName(Type type)
		{
			if (type.Namespace == "DialogSystem.Nodes")
			{
				return base.GetNodeMenuName(type).Replace("DialogSystem/Nodes/", "");
			}

			return null;
		}

		public override void AddContextMenuItems(GenericMenu menu)
		{
			Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);
			for (int i = 0; i < NodeEditorWindow.nodeTypes.Length; i++)
			{
				Type type = NodeEditorWindow.nodeTypes[i];

				//Get node context menu path
				string path = GetNodeMenuName(type);
				if (string.IsNullOrEmpty(path)) continue;

				if (!path.Contains("Start Point"))
				{
					menu.AddItem(new GUIContent(path), false, () =>
					{
						CreateNode(type, pos);
					});
				}
			}
			menu.AddSeparator("");
			menu.AddItem(new GUIContent("Preferences"), false, () => NodeEditorWindow.OpenPreferences());
			NodeEditorWindow.AddCustomContextMenuItems(menu, target);
		}
	}
}