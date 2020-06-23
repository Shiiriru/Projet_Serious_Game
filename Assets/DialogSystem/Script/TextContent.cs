﻿using UnityEngine;
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
		[SerializeField] Text textContentHeight;

		public bool isDisplayFinished { get; private set; }

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		public void SetDisplaySpeed(int speed)
		{
			playSpeed = 0.06f / speed;
		}

		public void SetText(string str, bool displayAll)
		{
			SetDisplayFinished(displayAll ? true : false);
			targetStr = str;

			text.text = displayAll ? targetStr : "";
			if (!displayAll)
				text.DOText(targetStr, targetStr.Length * playSpeed).OnComplete(() => SetDisplayFinished(true));

			if (autoHeight)
				StartCoroutine(SetContentHeightCoroutine(str));
		}

		protected virtual IEnumerator SetContentHeightCoroutine(string str)
		{
			textContentHeight.text = str;
			yield return null;
			DOTween.Kill(rectTransform);
			rectTransform.DOSizeDelta(new Vector2(rectTransform.sizeDelta.x, textContentHeight.rectTransform.sizeDelta.y + 50), 0.1f);
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
			DOTween.Kill(canvasGroup);

			if (canvasGroup.alpha != alpha)
			{
				if (doFade)
					canvasGroup.DOFade(alpha, fadeTime).SetEase(Ease.InOutQuad);
				else
					canvasGroup.alpha = alpha;
			}
		}

		public bool isFading()
		{
			return DOTween.IsTweening(canvasGroup);
		}
	}
}