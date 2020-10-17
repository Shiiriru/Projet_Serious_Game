﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorScreen : MonoBehaviour
{
	Image img;
	Coroutine flashCoroutine;
	// Start is called before the first frame update
	void Awake()
	{
		img = GetComponent<Image>();
		img.color = Color.clear;
	}

	void SetColor(Color color)
	{
		img.color = color;
	}

	public void Show(Color color, float duration)
	{
		if (duration <= 0)
			SetColor(color);
		else
			ChangeColor(color, duration, true);
	}

	public void Hide(float duration)
	{
		var c = img.color;
		ChangeColor(new Color(c.r, c.g, c.b, 0), duration, false);
	}

	void ChangeColor(Color color, float duration, bool raycastTarget)
	{
		img.DOColor(color, duration);
		img.raycastTarget = raycastTarget;
	}

	public void Flash(float duration, Color color)
	{
		if (flashCoroutine != null)
			StopCoroutine(flashCoroutine);

		flashCoroutine = StartCoroutine(ScreenFlashCoroutine(duration, color));
	}

	IEnumerator ScreenFlashCoroutine(float duration, Color color)
	{
		SetColor(new Color(color.r, color.g, color.b, 0));
		Show(new Color(color.r, color.g, color.b, 1), 0.05f);
		yield return new WaitForSeconds(0.05f + duration);
		Hide(0.05f);
		yield return new WaitForSeconds(0.05f);
		flashCoroutine = null;
	}
}
