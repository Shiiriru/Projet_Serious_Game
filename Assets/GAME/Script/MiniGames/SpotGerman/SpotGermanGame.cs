using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGermanGame : MiniGameBase
{
	[SerializeField] GameObject foundText;

	private void Awake()
	{
		foundText.SetActive(false);
	}

	public override void Launch()
	{
		base.Launch();
	}

	public override void FnishGame()
	{
		if (gameFinished)
			return;
		foundText.SetActive(true);

		StartCoroutine(GameFinishCoroutine());
		gameFinished = true;
	}

	IEnumerator GameFinishCoroutine()
	{
		yield return new WaitForSeconds(2f);
		OnGameComplete();
		gameObject.SetActive(false);
	}
}
