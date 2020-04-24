using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationItem : MonoBehaviour
{
    public Item item;

    public GameObject Bouton;
    public GameObject FicheTech;

    /*public Image illustration;
    public Text Titre;

    public FicheTechManager ContenuTexte;*/

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

        /*ContenuTexte.LectureTexte(item.TexteObject); //Je récupère le texte dans le script Item

        illustration.sprite = item.image;
        illustration.enabled = true;

        Titre.text = item.name;*/
    }
}
