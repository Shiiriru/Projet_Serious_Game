using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Canvas))]
public class UIMain : MonoBehaviour
{
	[SerializeField] Image foregourndBg;
	[SerializeField] DatePanel datePanel;

	Canvas canvas;
	[SerializeField] UIPlayer uiPlayer;
	public UIPlayer UIPlayer { get { return uiPlayer; } }

	[SerializeField] [EventRef] string soundSwithScene;
	public event System.Action onChangeSceneFinished;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();
		foregourndBg.color = Color.clear;
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

	public void ChangeScene(string sceneName, float time, DatePanelInfosObject dateInfos = null)
	{
		StartCoroutine(ChangeSceneCoroutine(sceneName, time, dateInfos));
	}

	IEnumerator ChangeSceneCoroutine(string sceneName, float time, DatePanelInfosObject dateInfos)
	{
		foregourndBg.DOColor(Color.black, 0.8f);
		yield return new WaitWhile(() => DOTween.IsTweening(foregourndBg));

		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		if (dateInfos != null)
		{
			datePanel.Launch(dateInfos, time);
			yield return new WaitWhile(() => datePanel.launchCoroutine != null);
		}
		else
		{
			yield return new WaitForSeconds(time);
		}

		foregourndBg.DOColor(Color.clear, 0.8f);
		yield return new WaitWhile(() => DOTween.IsTweening(foregourndBg));

		if (onChangeSceneFinished != null)
		{
			onChangeSceneFinished();
			onChangeSceneFinished = null;
		}
	}

	public void ShowPlayerUI(bool show)
	{
		uiPlayer.Show(show);
	}

	public void OpenFicheTemplate(ItemInfoObject item, StudioEventEmitter emitter = null)
	{
		uiPlayer.OpenFicheTemplate(item, emitter);
	}

	public void SetFicheTemplateCustomAction(Sprite sprite, System.Action action)
	{
		uiPlayer.SetFicheTemplateCustomAction(sprite, action);
	}

	public void CloseFicheTemplate()
	{
		uiPlayer.CloseFicheTemplate();
	}

	public void AddToInventoryScreen(ItemInfoObject itemInfo)
	{
		uiPlayer.GetItem(itemInfo);
	}
}
