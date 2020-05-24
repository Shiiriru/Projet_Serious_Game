using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheTemplate : MonoBehaviour
{
    //public Item Infoitem;

    public GameObject PageWiki;
    public GameObject PageObjet;

    [SerializeField] GameObject ButtonPhoneCall;
    [SerializeField] GameObject ButtonDeployMap;
    public GameObject BoutonInventaire;

    public Image illustration;
    public Image photoarchive;
    public Text Titre;

    public FicheTechManager ContenuTexte;
    public InfoItemBouton SoundManager;

    public void OpenPageObj(Item item, bool gotoinventory) //(Je récupère toutes les valeurs lié à item et je récupère la valeur du bool, j'indique ici les éléments que je souhaite envoyer. Tout ce qui est entre parenthèse sont mes variables en local)
    {
        gameObject.SetActive(true);
        RetourPageObjet();

        ContenuTexte.LectureTexte(item.TexteObject); //Je récupère le texte dans le script Item 
        ContenuTexte.LectureWiki(item.TexteWiki);

        illustration.sprite = item.image;
        illustration.enabled = true;

        Titre.text = item.name;

        if (gotoinventory == false)
        {
            BoutonInventaire.SetActive(false);
        }
        else
        {
            BoutonInventaire.SetActive(true);
        }

        photoarchive.sprite = item.photo;

    }


    public void PageArchive()
    {
        PageObjet.SetActive(false);
        PageWiki.SetActive(true);
    }

    public void RetourPageObjet()
    {
        PageObjet.SetActive(true);
        PageWiki.SetActive(false);
    }

    public void ExitFiche()
    {
        gameObject.SetActive(false);
        ButtonPhoneCall.SetActive(false);
        ButtonDeployMap.SetActive(false);
    }
}
