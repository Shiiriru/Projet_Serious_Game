using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject BoutonSacOuverture;
    public GameObject BoutonNoteOuverture;
    public GameObject SacDeployed;
    public GameObject NoteDeployed;

    /*bool SacIsDeployed = false;
    bool NoteIsDeployed = false;*/

    // Start is called before the first frame update

    // Update is called once per frame

    public void DeployObjectSac()
    {
        SacDeployed.SetActive(true);
        BoutonSacOuverture.SetActive(false);
        //SacIsDeployed = true;

        if (NoteDeployed == true)
        {
            NoteDeployed.SetActive(false);
            BoutonNoteOuverture.SetActive(true);
        }
    }

    public void DeployObjectNote()
    {
        NoteDeployed.SetActive(true);
        BoutonNoteOuverture.SetActive(false);
        //NoteIsDeployed = true;

        if (SacDeployed == true)
        {
            SacDeployed.SetActive(false);
            BoutonSacOuverture.SetActive(true);
        }
    }

    public void CloseObject()
    {
        if(SacDeployed == true || NoteDeployed == true)
        {
            NoteDeployed.SetActive(false);
            BoutonNoteOuverture.SetActive(true);

            SacDeployed.SetActive(false);
            BoutonSacOuverture.SetActive(true);
        }
    }
}
