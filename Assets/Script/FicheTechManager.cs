using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheTechManager : MonoBehaviour
{
    //pas besoin de mettre le .txt ici car je le déclare dans item 
    public Text TexteDescription;

    private string TexteDefilement;
    private List<string> eachLine;

    Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LectureTexte(TextAsset textAsset)
    {
        TexteDescription.text = textAsset.text; //je remplace le texte par l'autre
    }
}
