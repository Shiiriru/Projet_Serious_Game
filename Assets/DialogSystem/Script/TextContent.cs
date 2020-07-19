using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace DialogSystem
{
	[RequireComponent(typeof(CanvasGroup))]
	public class TextContent : MonoBehaviour
	{
		protected RectTransform rectTransform;
		CanvasGroup canvasGroup;
		[SerializeField] Text text;
		string targetStr;
		float playSpeed;

		//for set auto height
		[SerializeField] bool autoHeight;
		[SerializeField] protected Text textContentHeight;
		//
		protected bool disableAnimateHeight;

		public bool isDisplayFinished { get; private set; }

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		public void SetDisplaySpeed(int speed)
		{
			playSpeed = 0.12f / (speed + (speed - 1) * 2f);
		}

		public void SetText(string str, bool displayAll)
		{
			SetDisplayFinished(displayAll ? true : false);
			targetStr = str;

			text.text = displayAll ? targetStr : "";
			if (!displayAll)
				text.DOText(targetStr, targetStr.Length * playSpeed).OnComplete(() => SetDisplayFinished(true)).SetEase(Ease.Linear);

			if (autoHeight)
				StartCoroutine(SetContentHeightCoroutine(str));
		}

		protected virtual IEnumerator SetContentHeightCoroutine(string str)
		{
			textContentHeight.text = str;
			yield return null;

			var targetSize = new Vector2(rectTransform.sizeDelta.x, textContentHeight.rectTransform.sizeDelta.y + 50);

			DOTween.Kill(rectTransform);
			if (disableAnimateHeight)
			{
				rectTransform.sizeDelta = targetSize;
				disableAnimateHeight = false;
			}
			else
				rectTransform.DOSizeDelta(targetSize, 0.1f);
		}

		public void DisplayEntierStr()
		{
			DOTween.Kill(text);
			text.text = targetStr;
			SetDisplayFinished(true);
		}

		void SetDisplayFinished(bool b)
		{
			isDisplayFinished = b;
		}

		public void Show(bool show, bool doFade, float fadeTime = 0.3f)
		{
			var alpha = show ? 1 : 0;

			if (canvasGroup.alpha == alpha)
				return;

			disableAnimateHeight = true;

			DOTween.Kill(canvasGroup);
			if (doFade)
				canvasGroup.DOFade(alpha, fadeTime).SetEase(Ease.InOutQuad);
			else
				canvasGroup.alpha = alpha;
		}

		public bool isFading()
		{
			return DOTween.IsTweening(canvasGroup);
		}
	}
}