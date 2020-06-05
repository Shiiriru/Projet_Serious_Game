using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class UIMain : MonoBehaviour
{
	Canvas canvas;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoadGetCamera;
	}

	void OnSceneLoadGetCamera(Scene scene, LoadSceneMode mode)
	{
		var cam = FindObjectOfType<Camera>();
		canvas.worldCamera = cam;
	}
}
