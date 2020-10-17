using DG.Tweening;
using effect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem
{
	public class ComicController : MonoBehaviour
	{
		CanvasGroup canvasGroup;
		[SerializeField] GameObject[] comicList;
		public GameObject[] ComicList { get { return comicList; } }

		public void Init(Transform parent)
		{
			foreach (var c in comicList)
				c.SetActive(false);

			transform.SetParent(parent, false);
			canvasGroup = gameObject.AddComponent<CanvasGroup>();
		}

		public void ShowComicObject(int index, float duration = 0.4f)
		{
			canvasGroup.interactable = true;
			canvasGroup.alpha = 1;

			var comic = comicList[index];
			if (comic.GetComponent<CanvasGroup>() != null)
			{
				comic.SetActive(true);
				var group = comic.GetComponent<CanvasGroup>();
				group.alpha = 0;
				group.DOFade(1, duration);
			}
			else if (comic.GetComponent<Image>() != null)
			{
				comic.SetActive(true);
				var img = comic.GetComponent<Image>();
				var c = img.color;
				img.color = new Color(c.r, c.g, c.b, 0);
				img.DOColor(new Color(c.r, c.g, c.b, 1), duration);
			}
		}

		public void HideComicObject(int index, float duration = 0.4f)
		{
			var comic = comicList[index];
			if (comic.GetComponent<CanvasGroup>() != null)
				comic.GetComponent<CanvasGroup>().DOFade(0, duration).OnComplete(Hide);
			else if (comic.GetComponent<Image>() != null)
			{
				var img = comic.GetComponent<Image>();
				var c = img.color;
				img.DOColor(new Color(c.r, c.g, c.b, 0), duration).OnComplete(Hide);
			}

			void Hide()
			{
				comic.SetActive(false);
			}
		}

		public void HideAll(float duration = 0.5f)
		{
			canvasGroup.interactable = false;
			canvasGroup.DOFade(0, duration);
		}

		//public void AddShakeEffect(int index)
		//{
		//	var comic = comicList[index];

		//	if (comic.GetComponent<ShakeEffect>() == null)
		//		comic.AddComponent<ShakeEffect>();
		//}
	}
}