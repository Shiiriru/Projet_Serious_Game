using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBureauxManager : MonoBehaviour
{
    [SerializeField] InfoItemBouton BoutonMap;
    [SerializeField] InfoItemBouton BoutonLetter;
    [SerializeField] ButtonBase BoutonArtilery;

    [SerializeField] GameObject ButtonExit;

    public bool MapIsChecked { get; private set; } //get le bool mais on ne peut pas le modifier ailleurs
    private bool LetterIsChecked;
    private bool ArtileryIsChecked;

    // Start is called before the first frame update
    void Start()
    {
        BoutonMap.OnChecked += CheckMapBoolean; //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
        BoutonLetter.OnChecked += CheckLetterBoolean;
        BoutonArtilery.OnChecked += CheckArtileryBoolean;

        if (MapIsChecked == true && LetterIsChecked == true && ArtileryIsChecked == true)
        {
            ExitLevel();
        }
    }

    private void CheckMapBoolean()
    {
        MapIsChecked = true;
        Debug.Log(MapIsChecked);
    }

    private void CheckLetterBoolean()
    {
        LetterIsChecked = true;
        Debug.Log(LetterIsChecked);
    }

    private void CheckArtileryBoolean()
    {
        ArtileryIsChecked = true;
        Debug.Log(ArtileryIsChecked);
    }

    private void ExitLevel()
    {
        ButtonExit.SetActive(true);
    }
}
