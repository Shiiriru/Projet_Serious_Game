using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	[SerializeField] Image icon;

	public InventoryItemInfoObject CurrrentInfoItem { get; private set; }
	UIPlayer uiPlayer;

	public void Init(UIPlayer _uiPlayer)
	{
		uiPlayer = _uiPlayer;
		LoseItem();
	}

	public void GetItem(InventoryItemInfoObject newItem)
	{
		CurrrentInfoItem = newItem;

		icon.sprite = CurrrentInfoItem.imageInventory;
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
				uiPlayer.OpenFicheTemplate(CurrrentInfoItem, null, false);
				break;
			case InventoryItemType.Photo:
				uiPlayer.ShowPhoto(CurrrentInfoItem.imageResume != null ? CurrrentInfoItem.imageResume : CurrrentInfoItem.imageInventory);
				break;
		}
	}


	public bool CanAdd(InventoryItemInfoObject item)
	{
		return CurrrentInfoItem == null && CurrrentInfoItem != item;
	}
}
