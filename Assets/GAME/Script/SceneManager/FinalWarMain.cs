using DG.Tweening;
using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWarMain : MonoBehaviour
{
	UIMain uiMain;

	[SerializeField] DialogGraph dialogFinalWar;
	[SerializeField] DialogGraph dialogAfterWar;

	[SerializeField] [EventRef] string FinalWarAmbiance;
	[SerializeField] [EventRef] string AfterWarAmbiance;

	[SerializeField] SceneReference sceneCredit;
	private void Start()
	{
		uiMain = FindObjectOfType<UIMain>();

		uiMain.onChangeSceneFinished += Init;
	}

	private void Init()
	{
		DialogPlayerHelper.SetDialog(dialogFinalWar);
		DialogPlayerHelper.SetOnFinishedAction(PlayAfterWarDialog);

		SoundPlayer.PlayEvent("bg", FinalWarAmbiance);
	}

	void PlayAfterWarDialog()
	{
		StartCoroutine(AfterWarCoroutine());
	}

	IEnumerator AfterWarCoroutine()
	{
		yield return new WaitForSeconds(8f);
		SoundPlayer.PlayEvent("bg", AfterWarAmbiance);
		yield return new WaitForSeconds(10f);

		DialogPlayerHelper.SetDialog(dialogAfterWar);
		DialogPlayerHelper.SetOnFinishedAction(EndGame);
	}

	void EndGame()
	{
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
		SceneManager.LoadScene(sceneCredit, LoadSceneMode.Single);
	}
}
