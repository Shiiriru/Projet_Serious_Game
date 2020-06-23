using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditor;
using System.IO;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(StartPoint))]
	public class StartPointEditor : NodeEditor
	{
		DialogGraph graph;

		string variableSourcePath;
		Dictionary<string, System.Type> vSourceList = new Dictionary<string, System.Type>();
		Rect vSourceButtonRect;

		public override void OnCreate()
		{
			variableSourcePath = Application.dataPath + @"\DialogSystem\Script\VariableSource\";
			graph = ((target as Node).graph) as DialogGraph;
			base.OnCreate();
		}

		public override void AddContextMenuItems(GenericMenu menu) { }

		public override void OnBodyGUI()
		{
			serializedObject.Update();

			OnGuiVariableSource();
			GUILayout.Space(5);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("showPlayerUIAfterDialog"));

			GUILayout.Space(-20);
			NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
			serializedObject.ApplyModifiedProperties();
		}

		void OnGuiVariableSource()
		{
			if (graph.variableSource == null)
			{
				VariableSourcePopup();
				return;
			}

			GUILayout.BeginHorizontal();
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.ObjectField(graph.variableSource, typeof(VariableSourceObject), false);
			EditorGUI.EndDisabledGroup();

			GUI.backgroundColor = Color.red;
			if (GUILayout.Button("x", GUILayout.Width(25)))
			{
				RemoveVraibleSource();
			}
			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();

			//OnGUIEditActiveConditions();

			GUILayout.Space(5);
		}

		void VariableSourcePopup()
		{
			if (GUILayout.Button("Add VariableSourceObject"))
			{
				string[] conditionObjs =
					Directory.GetFiles(variableSourcePath, "*.cs", SearchOption.TopDirectoryOnly);
				vSourceList.Clear();

				foreach (string path in conditionObjs)
				{
					string assetPath = "Assets" + path.Replace(Application.dataPath, "").Replace('\\', '/');
					MonoScript obj = (MonoScript)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript));
					var objType = obj.GetClass();

					if (System.Activator.CreateInstance(objType) is VariableSourceObject)
						vSourceList.Add(objType.Name, objType);
				}

				var popup = new ConditionsObjectSelectorPopup();
				popup.Init(graph, vSourceList);

				PopupWindow.Show(vSourceButtonRect, popup);
			}

			if (Event.current.type == EventType.Repaint)
				vSourceButtonRect = GUILayoutUtility.GetLastRect();
		}

		void RemoveVraibleSource()
		{
			var obj = graph.variableSource;
			graph.RemoveVariableSource();
			Object.DestroyImmediate(obj, true);

			AssetDatabase.SaveAssets();
		}
	}
}