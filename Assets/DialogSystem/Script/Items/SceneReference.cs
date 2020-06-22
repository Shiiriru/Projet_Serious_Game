﻿using UnityEditor;
using UnityEngine;

namespace DialogSystem
{
	[System.Serializable]
	public class SceneReference
	{
		[SerializeField] private Object sceneAsset;
		[SerializeField] private string sceneName = "";

		public string Name
		{
			get { return sceneName; }
		}

		// makes it work with the existing Unity methods (LoadLevel/LoadScene)
		public static implicit operator string(SceneReference sceneField)
		{
			return sceneField.Name;
		}
	}
}