using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationItem : MonoBehaviour
{
    public Item item;

    public GameObject Bouton;
    public GameObject FicheTech;

    public void SelectionItem()
    {
        Debug.Log("Objet" + item.name + "récolté");
        bool IsSelected = InventoryScript.instance.Add(item, FicheTech);

        if(IsSelected)
        {
            Destroy(gameObject);
            Bouton.SetActive(false);
            Debug.Log("Objet" + item.name + "est détruit");
        }
    }
}
