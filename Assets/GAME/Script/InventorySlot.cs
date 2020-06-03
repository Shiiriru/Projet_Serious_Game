using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    GameObject Fichetech;

    Item infoItem;
    InfoItemBouton activation;
    public FicheTemplate RecupInfo;

    public void AddItem (Item newItem)
    {
        infoItem = newItem;

        icon.sprite = infoItem.icon;
        icon.enabled = true;


    }

    public void Clearslot()
    {
        infoItem = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(infoItem != null)
        {
            RecupInfo.OpenPageObj(infoItem, false, null);
        }
    }
}
