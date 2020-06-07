using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;

namespace DialogSystem
{
	[RequireComponent(typeof(CanvasGroup))]
	public class TextContent : MonoBehaviour
	{
		CanvasGroup canvasGroup;
		[SerializeField] Text text;
		string targetStr;
		float playSpeed;

		public bool isDisplayFinished { get; private set; }

		private void Awake()
		{
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