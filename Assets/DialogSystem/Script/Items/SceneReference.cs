using UnityEditor;
using UnityEngine;

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
	public static implicit operator string(SceneReference sceneRef)
	{
		return sceneRef.Name;
	}
}