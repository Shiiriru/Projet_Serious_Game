using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	public static InventoryScript instance;

	void Awake()
	{
		instance = this;
	}

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;


	public int space = 5;

	public List<InventoryItemInfoObject> items = new List<InventoryItemInfoObject>();

	public bool Add(InventoryItemInfoObject item)
	{
		if (!item.isDefaultItem)
		{
			if (items.Count >= space)
			{
				Debug.Log("Inventaire plein !");
				return false;
			}
			items.Add(item);

			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}
		}

		return true;
	}

	public void Remove(InventoryItemInfoObject item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}
}
