using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public GameObject PageObjet;
    public GameObject PageTech;

    // Update is called once per frame

    public void ActiveChamp()
    {
        PageObjet.SetActive(true);
    }

    public void PageArchive()
    {
        PageObjet.SetActive(false);
        PageTech.SetActive(true);
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
