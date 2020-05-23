using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace DialogSystem
{
	public class ConditionsObjectSelectorPopup : PopupWindowContent
	{
		DialogGroupItem group;
		Dictionary<string, Type> objList;
		public void Init(DialogGroupItem _group, Dictionary<string, Type> _objList)
		{
			group = _group;
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
			var conditionObj = group.AddConditionObject(type);

			if (string.IsNullOrEmpty(conditionObj.name))
			{
				string typeName = typeof(VariableSourceObject).Name;
				conditionObj.name = ObjectNames.NicifyVariableName(typeName);
			}
			AssetDatabase.AddObjectToAsset(conditionObj, group);
			AssetDatabase.SaveAssets();
		}
	}
}