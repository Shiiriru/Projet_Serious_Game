using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace DialogSystem
{
	public class ConditionsObjectSelectorPopup : PopupWindowContent
	{
		DialogGraph graph;
		Dictionary<string, Type> objList;
		public void Init(DialogGraph _graph, Dictionary<string, Type> _objList)
		{
			graph = _graph;
			objList = _objList;
		}

		public override void OnGUI(Rect rect)
		{
			foreach(var o in objList)
			{
				if (GUILayout.Button(o.Key))
				{
					CreateConditionsObject(o.Value);
					editorWindow.Close();
				}
			}
		}

		void CreateConditionsObject(Type type)
		{
			var conditionObj = graph.AddVariableSource(type);

			if (string.IsNullOrEmpty(conditionObj.name))
			{
				string typeName = typeof(VariableSourceObject).Name;
				conditionObj.name = ObjectNames.NicifyVariableName(typeName);
			}
			AssetDatabase.AddObjectToAsset(conditionObj, graph);
			AssetDatabase.SaveAssets();
		}
	}
}