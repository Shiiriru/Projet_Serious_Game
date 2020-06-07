using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FicheTemplate : MonoBehaviour
{
	[SerializeField] Button buttonAddToInventory;

	StudioEventEmitter soundEmitter;

	[SerializeField] Text textTitle;
	[SerializeField] Image imgPhoto;

	[SerializeField] Text textInfos;
	[SerializeField] RectTransform textInfosContent;

	InventoryItemInfoObject targetInfoItem;
	InfoItemBouton sceneInfoItem;

	[SerializeField] InventoryScript inventory;

	public event System.Action onClose;

	[SerializeField] CustomActionButton[] customActionButtons;

	//(Je récupère toutes les valeurs lié à item et je récupère la valeur du bool, j'indique ici les éléments que je souhaite envoyer. Tout ce qui est entre parenthèse sont mes variables en local)
	public void Open(InventoryItemInfoObject item, InfoItemBouton sceneItem, bool canAddToinventory,
		StudioEventEmitter emitter = null, Dictionary<string, System.Action> customActions = null)
	{
		Show(true);

		textTitle.text = item.name;
		targetInfoItem = item;
		sceneInfoItem = sceneItem;
		buttonAddToInventory.interactable = canAddToinventory;

		SetCustomButtons(customActions);

		soundEmitter = emitter;
		if (soundEmitter != null)
		{
			try
			{ soundEmitter.Play(); }
			catch { }
		}

		OnClickShowResume();
	}

	void SetCustomButtons(Dictionary<string, System.Action> customActions)
	{
		if (customActions == null)
			return;

		for (int i = 0; i < customActions.Keys.Count; i++)
		{
			if (i < customActionButtons.Length - 1)
			{
				var item = customActions.ElementAt(i);
				customActionButtons[i].Init(item.Key, item.Value);
			}
		}
	}

	public void OnClickAddItemToInventory()
	{
		bool IsSelected = inventory.Add(targetInfoItem);

		if (IsSelected)
		{
			sceneInfoItem.AddToInventory();
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

		if (onClose != null)		
			onClose();

		if (soundEmitter != null)
		{
			try { soundEmitter.Stop(); }
			catch { }
		}
	}

	void Show(bool isShow)
	{
		gameObject.SetActive(isShow);

		foreach (var c in customActionButtons)
			c.Show(false);
	}
}
