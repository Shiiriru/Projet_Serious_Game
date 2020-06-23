using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPaperScreen : MonoBehaviour
{
	[SerializeField] float maxScaleLevel = 3;
	[SerializeField] Transform scaleContent;
	[SerializeField] Image image;

	public void Open(Sprite sprite)
	{
		gameObject.SetActive(true);

		image.sprite = sprite;
		scaleContent.localScale = Vector3.one;
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	private void Update()
	{
		var scrollDelta = Input.mouseScrollDelta.y * 0.3f;
		if ((scrollDelta > 0 && scaleContent.localScale.x < maxScaleLevel) || (scrollDelta < 0 && scaleContent.localScale.x > 1))
		{
			var newScale = scaleContent.localScale + new Vector3(scrollDelta, scrollDelta);
			scaleContent.localScale = Vector3.Lerp(scaleContent.localScale, newScale, 10 * Time.deltaTime);
		}

	}
}
