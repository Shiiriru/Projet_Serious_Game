using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	[SerializeField] Image icon;

	public ItemInfoObject CurrrentInfoItem { get; private set; }
	UIPlayer uiPlayer;

	public void Init(UIPlayer _uiPlayer)
	{
		uiPlayer = _uiPlayer;
		LoseItem();
	}

	public void GetItem(ItemInfoObject newItem)
	{
		CurrrentInfoItem = newItem;

		icon.sprite = CurrrentInfoItem.imageResume;
		icon.enabled = true;
	}

	public void LoseItem()
	{
		CurrrentInfoItem = null;
		icon.enabled = false;
	}

	public void OnClickUseItem()
	{
		if (CurrrentInfoItem == null)
			return;
		switch (CurrrentInfoItem.itemType)
		{
			case InventoryItemType.Other:
				break;
			case InventoryItemType.InfoItem:
				uiPlayer.OpenFicheTemplate(CurrrentInfoItem);
				break;
			case InventoryItemType.Photo:
				uiPlayer.ShowPhoto(CurrrentInfoItem.imageResume);
				break;
		}
	}


	public bool CanAdd(ItemInfoObject item)
	{
		return CurrrentInfoItem == null && CurrrentInfoItem != item;
	}
}
