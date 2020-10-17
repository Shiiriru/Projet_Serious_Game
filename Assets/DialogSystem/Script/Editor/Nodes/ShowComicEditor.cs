

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogSystem.Nodes
{
	[CustomNodeEditor(typeof(ShowComicNode))]
	public class ShowComicEditor : DialogNodeEditorBase
	{
		ShowComicNode graph;

		public override void OnCreate()
		{
			graph = target as ShowComicNode;
			base.OnCreate();
		}

		public override void OnBodyGUI()
		{
			serializedObject.Update();
			DrawPorts();

			EditorGUILayout.PropertyField(serializedObject.FindProperty("comicPrefab"));
			if (graph.comicPrefab != null && graph.comicPrefab.GetComponent<ComicController>() == null)	
				graph.comicPrefab = null;

			if (graph.comicPrefab != null)
			{
				List<string> indexList = new List<string>();
				for (int i = 0; i < graph.comicPrefab.GetComponent<ComicController>().ComicList.Length; i++)
					indexList.Add(i.ToString());

				graph.index = EditorGUILayout.Popup("index", graph.index, indexList.ToArray());

				DrawDurationField();
				EditorGUILayout.PropertyField(serializedObject.FindProperty("isWait"));
			}

			serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(target);
		}
	}
}