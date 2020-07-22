using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	//this list show the item already added to inventory 
	//avoid reshow the item
	List<ItemInfoObject> addedInInventoryItems = new List<ItemInfoObject>();

	[SerializeField] UIPlayer uiPlayer;
	[SerializeField] InventorySlot[] inventorySlots;

	public event System.Action<ItemInfoObject> onAddItem;
	public event System.Action onItemChanged;

	public event System.Action onOpen;
	public event System.Action onClose;

	public void Init()
	{
		foreach (var s in inventorySlots)
			s.Init(uiPlayer);

		Show(false);
	}

	public bool Add(ItemInfoObject item)
	{
		foreach (var slot in inventorySlots)
		{
			if (slot.CanAdd(item))
			{
				addedInInventoryItems.Add(item);
				slot.GetItem(item);

				if (onAddItem != null)
					onAddItem(item);

				if (onItemChanged != null)
					onItemChanged();

				return true;
			}
		}

		Debug.Log("Inventaire plein !");
		return false;
	}

	public void Remove(string id)
	{
		InventorySlot targetSlot = null;
		foreach (var s in inventorySlots)
		{
			if (s.CurrrentInfoItem != null && s.CurrrentInfoItem.Id == id)
			{
				targetSlot = s;
				break;
			}
		}

		RemoveObject(targetSlot);
	}

	public void Remove(ItemInfoObject item)
	{
		InventorySlot targetSlot = null;
		foreach (var s in inventorySlots)
		{
			if (s.CurrrentInfoItem != null && s.CurrrentInfoItem == item)
			{
				targetSlot = s;
				break;
			}
		}

		RemoveObject(targetSlot);
	}

	void RemoveObject(InventorySlot targetSlot)
	{
		if (targetSlot == null)
			return;

		targetSlot.LoseItem();

		if (onItemChanged != null)
		{
			onItemChanged();
		}
	}

	public void Show(bool isShow)
	{
		if (isShow)
		{
			onOpen.Invoke();
		}
		else
		{
			onClose.Invoke();
		}

		gameObject.SetActive(isShow);
	}

	public bool CheckHasObject(string id)
	{
		return inventorySlots.Any(i => i.CurrrentInfoItem != null && i.CurrrentInfoItem.Id == id);
	}

	public bool AlreadyPicked(ItemInfoObject item)
	{
		return addedInInventoryItems.Contains(item);
	}
}
