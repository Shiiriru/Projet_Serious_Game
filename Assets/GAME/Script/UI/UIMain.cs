using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class UIMain : MonoBehaviour
{
	Canvas canvas;
	[SerializeField] Animator FadeInScreen;
	[SerializeField] GameObject PanelDate;

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

	IEnumerator LaunchPanelInfo()
	{
		yield return new WaitForSeconds(2f);
		ApparitionPanel();
	}

	void ApparitionPanel()
	{
		PanelDate.SetActive(true);
		StartCoroutine(LaunchEndOnboarding());
	}

	IEnumerator LaunchEndOnboarding()
	{
		yield return new WaitForSeconds(2f);

		PanelDate.SetActive(false);
		FadeInScreen.SetTrigger("FirstSpeechDone");
	}
}
