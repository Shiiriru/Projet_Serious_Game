using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationItem : MonoBehaviour
{
    public Item item;

    public GameObject TargetObject;
    public GameObject FicheTech;

    public void SelectionItem()
    {
        Debug.Log("Objet" + item.name + "récolté");
        bool IsSelected = InventoryScript.instance.Add(item, FicheTech);

        if(IsSelected)
        {
            gameObject.SetActive(false);
            TargetObject.SetActive(false);
            Debug.Log("Objet" + item.name + "est détruit");
        }
    }
}
