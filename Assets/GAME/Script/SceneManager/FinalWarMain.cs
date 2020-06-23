using DG.Tweening;
using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWarMain : MonoBehaviour
{
	UIMain uiMain;

	[SerializeField] DialogGraph dialogFinalWar;
	[SerializeField] DialogGraph dialogAfterWar;

	[SerializeField] CanvasGroup gameEndTextGroup;
	[SerializeField] SceneReference sceneMainMenu;
	private void Start()
	{
		uiMain = FindObjectOfType<UIMain>();

		DialogPlayerHelper.SetDialog(dialogFinalWar);
		DialogPlayerHelper.SetOnFinishedAction(PlayAfterWarDialog);
	}

	void PlayAfterWarDialog()
	{
		StartCoroutine(AfterWarCoroutine());
	}

	IEnumerator AfterWarCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		DialogPlayerHelper.SetDialog(dialogAfterWar);
		DialogPlayerHelper.SetOnFinishedAction(EndGame);
	}

	void EndGame()
	{
		StartCoroutine(EndGameCorutine());
	}

	IEnumerator EndGameCorutine()
	{
		yield return new WaitForSeconds(2f);
		gameEndTextGroup.DOFade(1, 3f);
		yield return new WaitForSeconds(5f);
		gameEndTextGroup.DOFade(0, 2f);
		yield return new WaitForSeconds(4f);
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
		SceneManager.LoadScene(sceneMainMenu, LoadSceneMode.Single);
	}
}
