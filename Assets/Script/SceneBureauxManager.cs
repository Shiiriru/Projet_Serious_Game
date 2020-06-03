using DialogSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SceneBureauxManager : MonoBehaviour
{
    [SerializeField] ButtonBase BoutonMap;
    [SerializeField] InfoItemBouton BoutonLetter;
    [SerializeField] ButtonBase BoutonArtilery;
    [SerializeField] InfoItemBouton BoutonPhoto;

    [SerializeField] GameObject ButtonExit;
    [SerializeField] DialogPlayer dialogplayer;

    public bool MapIsChecked { get; private set; } //get le bool mais on ne peut pas le modifier ailleurs
    private bool LetterIsChecked;
    private bool ArtileryIsChecked;
    private bool PhotoIsChecked;

    // Start is called before the first frame update
    void Start()
    {
        BoutonMap.OnChecked += CheckMapBoolean; //on fait appel à la variable, on ne l'ajoute pas. Permet d'ajouter plusieurs actions ensemble
        BoutonLetter.OnChecked += CheckLetterBoolean;
        BoutonArtilery.OnChecked += CheckArtileryBoolean;
        BoutonPhoto.OnChecked += CheckPhotoBoolean;
    }

    private void CheckMapBoolean()
    {
        MapIsChecked = true;
        Debug.Log(MapIsChecked);

        if (LetterIsChecked == true && ArtileryIsChecked == true)
        {
            ExitLevel();
        }
    }

    private void CheckLetterBoolean()
    {
        LetterIsChecked = true;
        Debug.Log(LetterIsChecked);

        if (MapIsChecked == true && ArtileryIsChecked == true)
        {
            ExitLevel();
        }
    }

    private void CheckArtileryBoolean()
    {
        ArtileryIsChecked = true;
        Debug.Log(ArtileryIsChecked);

        if (MapIsChecked == true && LetterIsChecked == true)
        {
            ExitLevel();
        }
    }

    private void CheckPhotoBoolean()
    {
        ((SceneTrancheeVariableSourceMng)(dialogplayer.VariableSourceMgr)).source.LetterChecked = true;
    }

    private void ExitLevel()
    {
        ButtonExit.SetActive(true);
        Debug.Log("Bouton loaded");
    }
}
