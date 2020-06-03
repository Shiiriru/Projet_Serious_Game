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

    public List<Item> items = new List<Item>();

    public bool Add(Item item, GameObject target)
    {
        if(!item.isDefaultItem)
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

        item.Fichetech = target;
        
            return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
