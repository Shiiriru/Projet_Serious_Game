using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DatePanel : MonoBehaviour
{
	CanvasGroup canvasGroup;

	[SerializeField] Text textDate;
	[SerializeField] Text textPlace;
	[SerializeField] Text textSoldatCount;

	public Coroutine launchCoroutine { get; private set; }

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
	}

	public void Launch(DatePanelInfosObject infos, float time)
	{
		textDate.text = infos.date;
		textPlace.text = infos.place;
		textSoldatCount.text = $"Soldats en vie : {infos.soldatCount}";

		StopAllCoroutines();
		launchCoroutine = StartCoroutine(LaunchCorutine(time));
	}

	IEnumerator LaunchCorutine(float displayTime)
	{
		yield return new WaitForSeconds(0.5f);
		canvasGroup.DOFade(1, 0.8f);

		yield return new WaitWhile(() => DOTween.IsTweening(canvasGroup));
		yield return new WaitForSeconds(displayTime);

		canvasGroup.DOFade(0, 0.8f);
		yield return new WaitWhile(() => DOTween.IsTweening(canvasGroup));
		yield return new WaitForSeconds(1);

		launchCoroutine = null;
	}
}
