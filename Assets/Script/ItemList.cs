using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{

    public enum ItemType
    {
        Masque,
        Photo,
        Journal,
        Map,
    }

    public ItemType itemList;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemList)
        {
            default:
            case ItemType.Masque: return SpriteManager.Instance.MasqueSprite;
            case ItemType.Photo: return SpriteManager.Instance.PhotoSprite;
            case ItemType.Journal: return SpriteManager.Instance.JournalSprite;
            case ItemType.Map: return SpriteManager.Instance.MapSprite;
        }
    }
}
