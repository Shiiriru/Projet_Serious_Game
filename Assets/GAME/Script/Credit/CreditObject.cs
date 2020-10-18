using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CreditObject : MonoBehaviour
{
	CanvasGroup canvasGroup;
	[SerializeField] float showTime = 2f;

	public event System.Action onPlayFinished;

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
	}

	public void Play()
	{
		gameObject.SetActive(true);
		StartCoroutine(PlayCoroutine());
	}

	IEnumerator PlayCoroutine()
	{
		canvasGroup.DOFade(1, 1f);
		yield return new WaitForSeconds(showTime + 1f);
		canvasGroup.DOFade(0, 1f);
		yield return new WaitForSeconds(1f);
		onPlayFinished.Invoke();
	}
}
