using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] SceneReference onBordingScene;
	[SerializeField] Image blackForeground;

	public void OnClickStart()
	{
		StartCoroutine(GameStartCoroutine());
	}

	IEnumerator GameStartCoroutine()
	{
		ShowForeGround();
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(onBordingScene.Name, LoadSceneMode.Single);
	}

    public void OnClickQuit()
	{
		StartCoroutine(QuitCoroutine());
	}

	IEnumerator QuitCoroutine()
	{
		ShowForeGround();
		yield return new WaitForSeconds(1.5f);
		Application.Quit();
	}

	void ShowForeGround()
	{
		blackForeground.DOColor(Color.black, 1f);
		blackForeground.raycastTarget = true;
	}
}
