using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomActionButton : MonoBehaviour
{
	RectTransform rectTransform;
	[SerializeField] Text text;
	Button button;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		button = GetComponent<Button>();
	}

	public void Init(string str, Action action)
	{
		text.text = str;
		button.onClick.AddListener(() => action());
		Show(true);
	}

	public void Show(bool show)
	{
		var targetX = show ? rectTransform.sizeDelta.x : 0;
		if (rectTransform.anchoredPosition.x == targetX)
			return;

		DOTween.Kill(rectTransform);
		rectTransform.DOAnchorPos( new Vector2(targetX, rectTransform.anchoredPosition.y), 0.3f).SetEase(Ease.OutQuad);
		if (!show)
			button.onClick.RemoveAllListeners();
	}
}
