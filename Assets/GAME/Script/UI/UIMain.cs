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

	[SerializeField] FicheTemplate ficheTemplate;

	[SerializeField] [EventRef] string soundSwithScene;

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

	public void SwitchScene(string sceneName, float time, DatePanelInfosObject dateInfos = null)
	{
		StartCoroutine(SwitchSceneCoroutine(sceneName, time, dateInfos));
	}

	IEnumerator SwitchSceneCoroutine(string sceneName, float time, DatePanelInfosObject dateInfos)
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
			FMODUnity.RuntimeManager.PlayOneShot(soundSwithScene);
			yield return new WaitForSeconds(time);
		}

		foregourndBg.DOColor(Color.clear, 0.8f);
		yield return new WaitWhile(() => DOTween.IsTweening(foregourndBg));
	}

	public void ShowPlayerUI(bool show)
	{
		uiPlayer.Show(show);
	}

	public void OpenFicheTemplate(InventoryItemInfoObject item, bool canAddToinventory, StudioEventEmitter emitter = null, InfoItemBouton sceneItem = null)
	{
		ficheTemplate.Open(item, canAddToinventory, emitter, sceneItem);
	}

	public void CloseFicheTemplate()
	{
		ficheTemplate.Close();
	}
}
