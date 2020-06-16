using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FicheTemplateButton : MonoBehaviour
{
	protected Button button;
	protected RectTransform rectTransform;

	[SerializeField] Color onSelectedColor;
	[SerializeField] protected Image imgIcon;
	public Image ImageIcon { get { return imgIcon; } }

	//hidding button position
	float defaultXPos;
	float defaultYPos;

	protected bool isShowing;

	private void Awake()
	{
		button = GetComponent<Button>();
		rectTransform = GetComponent<RectTransform>();
		defaultXPos = rectTransform.anchoredPosition.x - rectTransform.sizeDelta.x;
		defaultYPos = rectTransform.anchoredPosition.y;
		rectTransform.anchoredPosition = new Vector2(defaultXPos, defaultYPos);
	}

	public virtual void Show(bool show)
	{
		isShowing = show;
		DOTween.Kill(rectTransform);
		if (show)
			rectTransform.DOAnchorPos(new Vector2(-15, defaultYPos), 0.3f).SetEase(Ease.InOutCubic);
		else
			rectTransform.anchoredPosition = new Vector2(defaultXPos, defaultYPos);
	}

	public void OnSelected(bool selected)
	{
		if (!isShowing)
			return;

		DOTween.Kill(rectTransform);
		rectTransform.DOAnchorPos(new Vector2(selected ? 0 : -15, defaultYPos), 0.15f);
		button.Select();

		button.image.DOColor(selected ? onSelectedColor : Color.white, 0.15f);
	}
}
