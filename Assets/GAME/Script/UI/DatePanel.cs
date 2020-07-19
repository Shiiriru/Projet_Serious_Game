using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DatePanel : MonoBehaviour
{
	CanvasGroup canvasGroup;
	DatePanelInfosObject targetInfo;

	[SerializeField] Text textDate;
	[SerializeField] Text textPlace;
	[SerializeField] Text textSoldatCount;
	int soldatCount;

	public Coroutine launchCoroutine { get; private set; }

	[EventRef] public string launchEvt = "";

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		textSoldatCount.text = $"Soldats en vie : {soldatCount}";
	}

	public void Launch(DatePanelInfosObject infos, float time)
	{
		targetInfo = infos;
		textDate.text = targetInfo.date;
		textPlace.text = targetInfo.place;

		StopAllCoroutines();
		launchCoroutine = StartCoroutine(LaunchCorutine(time));
		SoundPlayer.PlayOneShot(launchEvt);
	}

	IEnumerator LaunchCorutine(float displayTime)
	{
		yield return new WaitForSeconds(0.5f);
		canvasGroup.DOFade(1, 0.8f);

		yield return new WaitWhile(() => DOTween.IsTweening(canvasGroup));

		while (soldatCount != targetInfo.soldatCount)
		{
			soldatCount = soldatCount + (soldatCount < targetInfo.soldatCount ? 1 : -1);
			textSoldatCount.text = $"Soldats en vie : {soldatCount}";
			yield return null;
		}

		yield return new WaitForSeconds(displayTime);

		canvasGroup.DOFade(0, 0.8f);
		yield return new WaitWhile(() => DOTween.IsTweening(canvasGroup));
		yield return new WaitForSeconds(1);

		launchCoroutine = null;
	}
}
