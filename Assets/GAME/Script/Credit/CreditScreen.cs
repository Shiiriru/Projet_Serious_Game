using DG.Tweening;
using DialogSystem.Nodes;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
	UIMain uiMain;
	[SerializeField] SceneReference mainMenuScene;

	[SerializeField] Image blackForeground;
	[SerializeField] CanvasGroup canvansFin;

	int creditStep = -1;

	[SerializeField] CreditObject[] creditList;
	[SerializeField] [EventRef] string musicPath;

	Coroutine playCoroutine;
	bool passCredit;

	private void Awake()
	{
		blackForeground.color = Color.clear;

		StartCredit();
		//uiMain = FindObjectOfType<UIMain>();
		//uiMain.onChangeSceneFinished += StartCredit;
	}

	private void Update()
	{
		if (passCredit || playCoroutine == null)
			return;

		if (Input.GetButtonDown("Fire1"))
		{
			passCredit = true;
			StopCoroutine(playCoroutine);
			playCoroutine = StartCoroutine(PassCreditCoroutine());
		}
	}

	private void StartCredit()
	{
		StartCoroutine(StartCreditCoroutine());
	}

	IEnumerator StartCreditCoroutine()
	{
		SoundPlayer.PlayEvent("bg", musicPath);
		yield return new WaitForSeconds(2f);
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
		PlayCredit();
	}

	IEnumerator PassCreditCoroutine()
	{
		blackForeground.DOColor(Color.black, 2f);
		SoundPlayer.StopEvent("bg", true);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(mainMenuScene.Name, LoadSceneMode.Single);
	}

	IEnumerator EndCreditCoroutine()
	{
		yield return new WaitForSeconds(2.5f);
		canvansFin.alpha = 1;
		yield return new WaitForSeconds(5f);
		canvansFin.alpha = 0;
		yield return new WaitForSeconds(2f);

		SoundPlayer.StopEvent("bg", true);
		blackForeground.color = Color.black;
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
	}

	void PlayCredit()
	{
		creditStep += 1;
		playCoroutine = StartCoroutine(PlayCreditCoroutine());
	}

	IEnumerator PlayCreditCoroutine()
	{
		if (creditStep > creditList.Length - 1)
		{
			playCoroutine = StartCoroutine(EndCreditCoroutine());
			yield break;
		}
		var credit = creditList[creditStep];
		credit.onPlayFinished += PlayCredit;

		yield return new WaitForSeconds(2.5f);
		credit.Play();
	}
}
