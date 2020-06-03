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

		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		public void SetText(string str)
		{
			text.text = "";
			text.DOText(str, str.Length * 0.01f);
		}

		public void Show(bool show)
		{
			var alpha = show ? 1 : 0;
			if (canvasGroup.alpha != alpha)
				canvasGroup.DOFade(alpha, 0.5f).SetEase(Ease.InOutQuad);
		}
	}
}