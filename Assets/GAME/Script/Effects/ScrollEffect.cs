using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace effect
{
	public class ScrollEffect : MonoBehaviour
	{
		[SerializeField] bool autoStart = true;
		bool startScroll;

		[SerializeField] Image[] images;
		[SerializeField] Vector2 startPos;
		[SerializeField] Vector2 endPos;

		[SerializeField] float speed = 10f;

		// Start is called before the first frame update
		void Start()
		{
			startScroll = autoStart;
		}

		private void Update()
		{
			if (!startScroll)
				return;

			foreach(var i in images)
			{
				i.rectTransform.anchoredPosition = Vector2.MoveTowards(i.rectTransform.anchoredPosition, endPos, speed * Time.deltaTime);
				if (Vector2.Distance(i.rectTransform.anchoredPosition, endPos) <= 1)
					i.rectTransform.anchoredPosition = startPos;
			}
		}
	}
}
