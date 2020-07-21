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
	[SerializeField] CanvasGroup buttonGroup;
	[SerializeField] Animator titleAnimator;

	[SerializeField] UISystem uiSystem;

	private void Start()
	{
		uiSystem.OnClickClose();

		blackForeground.color = Color.black;
		blackForeground.raycastTarget = true;

		buttonGroup.alpha = 0;

		StartCoroutine(ShowCoroutine());
	}

	IEnumerator ShowCoroutine()
	{
		yield return new WaitForSeconds(0.5f);

		blackForeground.DOColor(Color.clear, 2f);
		yield return new WaitForSeconds(2f);

		titleAnimator.SetTrigger("show");
		yield return new WaitForSeconds(2.5f);

		buttonGroup.DOFade(1, 1.5f);
		var btnRect = buttonGroup.GetComponent<RectTransform>();
		btnRect.DOAnchorPos(new Vector2(btnRect.anchoredPosition.x, btnRect.anchoredPosition.y - 15), 1.5f);

		yield return new WaitForSeconds(1f);
		blackForeground.raycastTarget = false;
	}

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

	public void OnClickOption()
	{
		uiSystem.Open(UISystem.Page.Options);
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
