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
		RectTransform rectTransform;
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

		IEnumerator SetContentHeightCoroutine(string str)
		{
			textContentHeight.text = str;
			yield return null;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, textContentHeight.rectTransform.sizeDelta.y + 50);
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

		public void Show(bool show)
		{
			var alpha = show ? 1 : 0;
			if (canvasGroup.alpha != alpha)
				canvasGroup.DOFade(alpha, 0.3f).SetEase(Ease.InOutQuad);
		}
	}
}