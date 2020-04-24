using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Item item;

    public GameObject PageObjet;
    public GameObject PageTech;
    public GameObject ActivationItemButton;

    public Image illustration;
    public Image Photoarchive;
    public Text Titre;

    public FicheTechManager ContenuTexte;
    public bool GoToInventory;

    // Update is called once per frame

    public void ActiveChamp()
    {
        PageObjet.SetActive(true);

        ContenuTexte.LectureTexte(item.TexteObject); //Je récupère le texte dans le script Item

        illustration.sprite = item.image;
        illustration.enabled = true;

        Titre.text = item.name;

        if (GoToInventory == false)
        {
            ActivationItemButton.SetActive(false);
        }
    }

    public void PageArchive()
    {
        PageObjet.SetActive(false);
        PageTech.SetActive(true);

        //ContenuTexte.LectureTexte(item.TexteObject); //Je récupère le texte dans le script Item

        Photoarchive.sprite = item.Photo;
        Photoarchive.enabled = true;
    }

    public void RetourObjet()
    {
        PageObjet.SetActive(true);
        PageTech.SetActive(false);
    }

    public void RetourGame()
    {
        PageObjet.SetActive(false);
        PageTech.SetActive(false);
    }
}
