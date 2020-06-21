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
		public void SetDisplaySide(DialogNode.DisplaySide side)
		{
			rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(side == DialogNode.DisplaySide.Left ? 0 : 1, 0);
			rectTransform.anchoredPosition = new Vector2(side == DialogNode.DisplaySide.Left ? 450 : -450, 25);
		}
	}
}