﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	List<InventoryItemInfoObject> inInventoryItems;

	[SerializeField] FicheTemplate ficheTemplate;
	[SerializeField] InventorySlot[] inventorySlots;

	public event System.Action onItemChanged;

	public void Init()
	{
		foreach (var s in inventorySlots)
			s.Init(ficheTemplate);

		Show(false);
		inInventoryItems = new List<InventoryItemInfoObject>();
	}

	public bool Add(InventoryItemInfoObject item)
	{
		foreach (var slot in inventorySlots)
		{
			if (slot.CanAdd(item))
			{
				inInventoryItems.Add(item);
				slot.GetItem(item);

				if (onItemChanged != null)
				{
					onItemChanged();
				}
				return true;
			}
		}

		Debug.Log("Inventaire plein !");
		return false;
	}

	public void Remove(InventoryItemInfoObject item)
	{
		InventorySlot targetSlot = null;
		foreach (var s in inventorySlots)
		{
			if (s.CurrrentInfoItem == item)
			{
				targetSlot = s;
				break;
			}
		}

		if (targetSlot == null)
			return;

		inInventoryItems.Remove(item);
		targetSlot.LoseItem();

		if (onItemChanged != null)
		{
			onItemChanged();
		}
	}

	public void Show(bool isShow)
	{
		gameObject.SetActive(isShow);
	}

	public bool CheckHasObject(string id)
	{
		return inInventoryItems.Any(i => i.Id == id);
	}
}
