using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheTemplate : MonoBehaviour
{
	//[SerializeField] GameObject ButtonPhoneCall;
	//[SerializeField] GameObject ButtonDeployMap;

	[SerializeField] Button buttonAddToInventory;

	StudioEventEmitter soundEmitter;

	[SerializeField] Text textTitle;
	[SerializeField] Image imgPhoto;

	[SerializeField] Text textInfos;
	[SerializeField] RectTransform textInfosContent;

	InventoryItemInfoObject targetInfoItem;
	InfoItemBouton sceneInfoItem;

	[SerializeField] InventoryScript inventory;

	private void Start()
	{
		Show(false);
	}

	public void Open(InventoryItemInfoObject item, bool canAddToinventory, StudioEventEmitter emitter = null, InfoItemBouton sceneItem = null) //(Je récupère toutes les valeurs lié à item et je récupère la valeur du bool, j'indique ici les éléments que je souhaite envoyer. Tout ce qui est entre parenthèse sont mes variables en local)
	{
		Show(true);
		soundEmitter = emitter;

		if (soundEmitter != null)
		{
			try
			{ soundEmitter.Play(); }
			catch { }
		}

		targetInfoItem = item;
		sceneInfoItem = sceneItem;

		textTitle.text = item.name;
		OnClickShowResume();

		buttonAddToInventory.interactable = canAddToinventory;
	}

	public void OnClickAddItemToInventory()
	{
		bool IsSelected = inventory.Add(targetInfoItem);

		if (IsSelected)
		{
			sceneInfoItem.Disable();
			buttonAddToInventory.interactable = false;
		}
		else
		{
			Debug.Log("inventory is full");
		}
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
		textInfosContent.sizeDelta = new Vector2(textInfosContent.sizeDelta.x, textInfos.rectTransform.sizeDelta.y + 20);
	}

	public void Close()
	{
		Show(false);
		//ButtonPhoneCall.SetActive(false);
		//ButtonDeployMap.SetActive(false);

		if (soundEmitter != null)
		{
			try { soundEmitter.Stop(); }
			catch { }
		}
	}

	void Show(bool isShow)
	{
		gameObject.SetActive(isShow);
	}
}
