using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	[SerializeField] Image icon;

	public InventoryItemInfoObject CurrrentInfoItem { get; private set; }
	FicheTemplate ficheTemplate;

	public void Init(FicheTemplate _ficheTemplate)
	{
		ficheTemplate = _ficheTemplate;
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
		if (CurrrentInfoItem != null)
		{
			ficheTemplate.Open(CurrrentInfoItem, false);
		}
	}

	public bool CanAdd(InventoryItemInfoObject item)
	{
		return CurrrentInfoItem == null && CurrrentInfoItem != item;
	}
}
