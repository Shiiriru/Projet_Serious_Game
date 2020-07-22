using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImageScreen : MonoBehaviour
{
	[SerializeField] float maxScaleLevel = 3;
	[SerializeField] RectTransform zoomContent;

	[SerializeField] Image image;

	public void Open(Sprite sprite)
	{
		gameObject.SetActive(true);
		image.sprite = sprite;
		zoomContent.localScale = Vector3.one;
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	private void Update()
	{
		if (DOTween.IsTweening(zoomContent.localScale))
			return;

		var scrollDelta = Input.mouseScrollDelta.y * 0.3f;
		if ((scrollDelta > 0 && zoomContent.localScale.x < maxScaleLevel) || (scrollDelta < 0 && zoomContent.localScale.x > 1))
		{
			var zoomFactor = scrollDelta / 6;
			zoomContent.localScale = zoomContent.localScale * (1 + zoomFactor);

			var mousePos = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(zoomContent, Input.mousePosition, Camera.main, out mousePos);

			ZoomContentToPos(mousePos, zoomFactor);
		}
	}

	public void OnClickZoom(bool zoomIn)
	{
		//cannot zoom
		if ((zoomIn && zoomContent.localScale.x >= maxScaleLevel) || (!zoomIn && zoomContent.localScale.x <= 1) ||
			DOTween.IsTweening(zoomContent.localScale))
			return;

		var zoomFactor = zoomIn ? 0.4f : -0.4f;
		Vector3 newScale = zoomContent.localScale * (1 + zoomFactor);
		newScale = new Vector3(Mathf.Clamp(newScale.x, 1, maxScaleLevel), Mathf.Clamp(newScale.x, 1, maxScaleLevel));

		var center = zoomContent.sizeDelta / (zoomContent.sizeDelta * newScale) / 2 * zoomContent.sizeDelta;
		center = new Vector2(center.x, -center.y);

		var duration = 0.2f;
		zoomContent.DOScale(newScale, duration);
		ZoomContentToPos(center, zoomFactor, true, duration);
	}

	void ZoomContentToPos(Vector2 center, float zoomFactor, bool animate = false, float duraton = 0.3f)
	{
		Vector2 direction = (center - zoomContent.anchoredPosition) * zoomFactor;

		var newPos = zoomContent.anchoredPosition - direction;
		if (!animate)
			zoomContent.anchoredPosition = newPos;
		else
			zoomContent.DOAnchorPos(newPos, duraton);
	}
}
