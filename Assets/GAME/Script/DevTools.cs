using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
	[SerializeField] SceneReference scBureau;
	[SerializeField] SceneReference scFinal;

	[SerializeField] GameObject mainPrefab;
	UIMain uiMain;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void ChangeChapter(int chapter)
	{
		SoundPlayer.StopAllEvent();

		//destroy old main and create new main
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
		var o = Instantiate(mainPrefab);
		uiMain = o.GetComponentInChildren<UIMain>();

		GameManager.chapterCount = chapter;
		StartCoroutine(ChangeChapterCoroutine());
	}

	IEnumerator ChangeChapterCoroutine()
	{
		yield return null;
		var varSource = (DialogPlayerHelper.VariableSourceMgr as MainVariableSourceMng).source;

		switch (GameManager.chapterCount)
		{
			case 0:
				uiMain.ChangeScene(scBureau.Name, 0);
				break;
			case 1:
				varSource.MaskChecked = true;
				uiMain.ChangeScene(scBureau.Name, 0);
				break;
			case 2:
				uiMain.ChangeScene(scBureau.Name, 0);
				break;
			case 3:
				uiMain.ChangeScene(scFinal.Name, 0);
				break;
		}
	}
}
