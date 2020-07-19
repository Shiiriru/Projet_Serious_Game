using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using DialogSystem.Nodes;

namespace DialogSystem
{
	public class DialogContent : TextContent
	{
		DialogNode.DisplaySide displaySide;
		public void SetDisplaySide(DialogNode.DisplaySide side)
		{
			if (displaySide == side)
				return;

			displaySide = side;
			disableAnimateHeight = true;
		}

		protected override IEnumerator SetContentHeightCoroutine(string str)
		{
			textContentHeight.text = str;
			yield return null;

			var targetSize = new Vector2(rectTransform.sizeDelta.x, textContentHeight.rectTransform.sizeDelta.y + 50);

			DOTween.Kill(rectTransform);
			//change side
			if (disableAnimateHeight)
			{
				rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(displaySide == DialogNode.DisplaySide.Left ? 0 : 1, 0);
				rectTransform.anchoredPosition = new Vector2(displaySide == DialogNode.DisplaySide.Left ? 450 : -450, 25);
				rectTransform.sizeDelta = targetSize;
				disableAnimateHeight = false;
			}
			else
				rectTransform.DOSizeDelta(targetSize, 0.1f);
		}
	}
}