using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackAnimation : MonoBehaviour
{
	[SerializeField] Image bg;
	[SerializeField] RectTransform content;
	[SerializeField] RectTransform buttonHit;
	[SerializeField] RectTransform buttonEnd;

	[SerializeField] RectTransform buttonCloseGame;

	[SerializeField] GameObject logoWin;
	[SerializeField] GameObject logoLose;

	public event System.Action onAnimationFinished;

	public void LaunchGame()
	{
		bg.color = Color.clear;
		content.anchoredPosition = new Vector2(0, 1100);
		buttonCloseGame.localScale = Vector3.zero;
		buttonCloseGame.gameObject.SetActive(false);

		logoWin.SetActive(false);
		logoLose.SetActive(false);
		StartCoroutine(LaunchGameCoroutine());
	}

	IEnumerator LaunchGameCoroutine()
	{
		bg.DOColor(new Color(0, 0, 0, 0.7f), 0.4f).OnComplete(() =>
		{
			buttonHit.DOAnchorPos(Vector2.zero, 0.3f).SetEase(Ease.OutBack);
			buttonEnd.DOAnchorPos(Vector2.zero, 0.3f).SetEase(Ease.OutBack);
			onAnimationFinished.Invoke();
		});
		yield return new WaitForSeconds(0.2f);
		content.DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack);
	}

	public void GameFinished(bool win)
	{
		buttonHit.DOAnchorPos(new Vector2(-330, 0), 0.2f);
		buttonEnd.DOAnchorPos(new Vector2(330, 0), 0.2f);
		buttonCloseGame.gameObject.SetActive(true);
		buttonCloseGame.DOScale(Vector3.one, 0.2f);

		logoWin.SetActive(win);
		logoLose.SetActive(!win);
	}

	public void CloseGame()
	{
		content.DOAnchorPos(new Vector2(0, 1100), 0.3f).SetEase(Ease.InBack);
		bg.DOColor(Color.clear, 0.4f).OnComplete(() =>
		{
			gameObject.SetActive(false);
			onAnimationFinished.Invoke();
		});
	}
}
