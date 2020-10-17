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
	[SerializeField] ComicManager comicManager;

	[SerializeField] ColorScreen foregourndBg;
	[SerializeField] DatePanel datePanel;

	Canvas canvas;
	[SerializeField] UIPlayer uiPlayer;
	public UIPlayer UIPlayer { get { return uiPlayer; } }

	[EventRef] public string ChangeChapterEvt = "";
	[EventRef] public string EndChapterEvt = "";

	public bool IsChangingScene { get; private set; }
	public event System.Action onChangeSceneFinished;

	[SerializeField] GetItemScreen getItemScreen;
	[SerializeField] Transform comicParent;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();

		uiPlayer.Inventory.onAddItem += ShowGetItemScreen;
		getItemScreen.Close();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoadGetCamera;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoadGetCamera;
	}

	void OnSceneLoadGetCamera(Scene scene, LoadSceneMode mode)
	{
		SceneManager.SetActiveScene(scene);

		var cam = FindObjectOfType<Camera>();
		canvas.worldCamera = cam;
	}

	public void ChangeScene(string sceneName, float time, DatePanelInfosObject dateInfos = null)
	{
		if (IsChangingScene)
			return;
		IsChangingScene = true;
		StartCoroutine(ChangeSceneCoroutine(sceneName, time, dateInfos));
	}

	IEnumerator ChangeSceneCoroutine(string sceneName, float time, DatePanelInfosObject dateInfos)
	{
		foregourndBg.Show(Color.black, 0.8f, foregourndBg.CurrentColor.a == 0);
		yield return new WaitForSeconds(0.8f);

		SoundPlayer.StopEvent("bg", true);
		yield return new WaitForSeconds(0.5f);
		comicManager.Clear();
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

		foregourndBg.Hide(0.8f);
		yield return new WaitForSeconds(0.8f);

		if (onChangeSceneFinished != null)
		{
			onChangeSceneFinished();
			onChangeSceneFinished = null;
		}

		IsChangingScene = false;
	}

	public void ShowPlayerUI(bool show)
	{
		uiPlayer.Show(show);
	}

	public void OpenFicheTemplate(ItemInfoObject item)
	{
		uiPlayer.OpenFicheTemplate(item);
	}

	public void SetFicheTemplateCustomAction(Sprite sprite, System.Action action)
	{
		uiPlayer.SetFicheTemplateCustomAction(sprite, action);
	}

	public void CloseFicheTemplate()
	{
		uiPlayer.CloseFicheTemplate();
	}

	public void AddToInventory(ItemInfoObject itemInfo)
	{
		uiPlayer.Inventory.Add(itemInfo);
	}

	private void ShowGetItemScreen(ItemInfoObject itemInfo)
	{
		getItemScreen.Open(itemInfo.imageInventory, itemInfo.name);
	}
}
