﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
	[SerializeField] SceneReference mainMenuScene;

	[SerializeField] GameObject blackScreen;

	[SerializeField] Image icanLogo;

	[SerializeField] Text title0;
	[SerializeField] Text title1;

	Coroutine displayLogoCoroutine;
	bool canPass;
	// Start is called before the first frame update
	void Start()
	{
		icanLogo.color = new Color(1, 1, 1, 0);
		title0.text = "";
		title1.text = "";

		displayLogoCoroutine = StartCoroutine(DisplayLogoCoroutine());
	}

	IEnumerator DisplayLogoCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		blackScreen.SetActive(false);

		yield return new WaitForSeconds(1f);

		icanLogo.DOColor(Color.white, 1.5f);
		yield return new WaitForSeconds(3f);
		icanLogo.DOColor(new Color(1, 1, 1, 0), 1.5f);

		canPass = true;
		yield return new WaitForSeconds(3f);

		title0.DOText("CYRILLE ET SIJIA", 1.5f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(1.8f);
		title1.DOText("PRESENTE", 0.5f).SetEase(Ease.Linear);

		yield return new WaitForSeconds(2f);

		//dotween bug when reduce text :/
		while (title0.text.Length > 0)
		{
			title0.text = title0.text.Remove(title0.text.Length - 1);
			if (title1.text.Length > 0)
				title1.text = title1.text.Remove(title1.text.Length - 1);
			yield return new WaitForSeconds(0.05f);
		}

		yield return new WaitForSeconds(1f);
		blackScreen.SetActive(true);

		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(mainMenuScene.Name, LoadSceneMode.Single);
	}

	private void Update()
	{
		if (!canPass || displayLogoCoroutine == null)
			return;

		if (Input.GetButtonDown("Fire1"))
		{
			StopCoroutine(displayLogoCoroutine);
			displayLogoCoroutine = null;

			SceneManager.LoadScene(mainMenuScene.Name, LoadSceneMode.Single);
		}
	}
}
