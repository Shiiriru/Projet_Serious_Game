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
		canvasGroup.alpha = 1;
		yield return new WaitForSeconds(showTime);
		canvasGroup.alpha = 0;
		onPlayFinished.Invoke();
	}
}
