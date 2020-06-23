using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FicheTemplate : MonoBehaviour
{
	[SerializeField] Text textTitle;
	[SerializeField] Image imgPhoto;

	[SerializeField] Text textInfos;
	[SerializeField] RectTransform textInfosContent;

	ItemInfoObject targetInfoItem;

	[SerializeField] InventoryScript inventory;

	public event System.Action onClose;

	[SerializeField] FicheTemplateButton resumeButton;
	[SerializeField] CustomActionButton customActionButton;

	[SerializeField] FicheTemplateButton[] btnList;

	private void Start()
	{
		foreach (var b in btnList)
			b.GetComponent<Button>().onClick.AddListener(() => SelecteButton(b));
	}

	// selete button animation
	private void SelecteButton(FicheTemplateButton b)
	{
		foreach (var btn in btnList)
			btn.OnSelected(btn == b);
	}

	//(Je récupère toutes les valeurs lié à item et je récupère la valeur du bool, j'indique ici les éléments que je souhaite envoyer. Tout ce qui est entre parenthèse sont mes variables en local)
	public void Open(ItemInfoObject item)
	{
		Show(true);

		textTitle.text = item.name;
		targetInfoItem = item;

		resumeButton.ImageIcon.sprite = item.imageResume;

		foreach (var b in btnList)
		{
			if (b != customActionButton)
				b.Show(true);
		}

		SelecteButton(resumeButton);

		OnClickShowResume();
	}

	public void SetCustomAction(Sprite sprite, System.Action action)
	{
		customActionButton.Init(sprite, action);
	}

	public void OnClickShowResume()
	{
		ShowImg(targetInfoItem.imageResume);
		SetText(targetInfoItem.textResume.text);
	}

	public void OnClickShowWiki()
	{
		ShowImg(targetInfoItem.imageWIKI);
		SetText(targetInfoItem.TexteWiki.text);
	}

	void ShowImg(Sprite sprite)
	{
		if (sprite == null)
			imgPhoto.enabled = false;

		imgPhoto.sprite = sprite;
		imgPhoto.enabled = true;
	}

	void SetText(string str)
	{
		textInfos.text = str;
		StartCoroutine(SetTextContentHeight());
	}

	IEnumerator SetTextContentHeight()
	{
		yield return null;
		textInfosContent.sizeDelta = new Vector2(textInfosContent.sizeDelta.x, textInfos.rectTransform.sizeDelta.y + 60);
	}

	public void Close()
	{
		Show(false);

		if (onClose != null)
			onClose();
	}

	void Show(bool isShow)
	{
		gameObject.SetActive(isShow);
		foreach (var b in btnList)
			b.Show(false);
	}
}
