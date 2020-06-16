using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomActionButton : FicheTemplateButton
{
	public void Init(Sprite sprite, Action action)
	{
		imgIcon.sprite = sprite;
		button.onClick.AddListener(() => action());
		Show(true);
	}

	public override void Show(bool show)
	{
		base.Show(show);

		if (!show)
			button.onClick.RemoveAllListeners();
	}
}
