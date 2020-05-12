using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{

    public enum ItemType
    {
        Telephone,
        Gramophone,
        Photo,
        Journal,
        Map,
        Lettre,
        Pholettre,
    }

    public ItemType itemList;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemList)
        {
            default:
            case ItemType.Telephone: return SpriteManager.Instance.TelephoneSprite;
            case ItemType.Gramophone: return SpriteManager.Instance.GramophoneSprite;
            case ItemType.Photo: return SpriteManager.Instance.PhotoSprite;
            case ItemType.Journal: return SpriteManager.Instance.JournalSprite;
            case ItemType.Map: return SpriteManager.Instance.MapSprite;
            case ItemType.Lettre: return SpriteManager.Instance.LettreSprite;
            case ItemType.Pholettre: return SpriteManager.Instance.PholettreSprite;
        }
    }
}
