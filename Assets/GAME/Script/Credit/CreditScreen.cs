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
	[SerializeField] SceneReference mainMenuScene;

	[SerializeField] Image blackForeground;
	[SerializeField] CanvasGroup canvansFin;

	[SerializeField] [EventRef] string musicPath;
	int creditStep = -1;

	[SerializeField] CreditObject[] creditList;

	Coroutine playCoroutine;
	bool passCredit;

	private void Start()
	{
		blackForeground.color = Color.clear;
		StartCoroutine(StartCreditCoroutine());
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

	IEnumerator StartCreditCoroutine()
	{
		yield return new WaitForSeconds(4f);
		PlayCredit();
	}

	IEnumerator PassCreditCoroutine()
	{
		blackForeground.DOColor(Color.black, 2f);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(mainMenuScene.Name, LoadSceneMode.Single);
	}

	IEnumerator EndCreditCoroutine()
	{
		yield return new WaitForSeconds(1f);
		canvansFin.DOFade(1, 3f);
		yield return new WaitForSeconds(5f);
		SoundPlayer.StopEvent("bg", true);
		canvansFin.DOFade(0, 2f);

		yield return new WaitForSeconds(4f);
		blackForeground.color = Color.black;
		yield return new WaitForSeconds(3f);
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

		yield return new WaitForSeconds(1.5f);
		credit.Play();
	}

}
