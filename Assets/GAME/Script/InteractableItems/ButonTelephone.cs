﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ButonTelephone : InfoItemBouton
{
    [SerializeField] GameObject ListCall;
    //[SerializeField] GameObject ButtonCall;
    //[SerializeField] GameObject ButtonCallRegiment;
    //[SerializeField] GameObject ButtonCallArtilery;

    [SerializeField] SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        uiMain.OpenFicheTemplate(Infoitem, false, soundEmitter);
        //ButtonCall.SetActive(true);
    }

    public void CallPhone()
    {
        uiMain.CloseFicheTemplate();
        //ListCall.SetActive(true);
        //ButtonCall.SetActive(false);
    }

    public void SwitchButton()
    {
        //if (sceneBureaux.MapIsChecked)
        //{
        //    ButtonCallRegiment.SetActive(false);
        //    ButtonCallArtilery.SetActive(true);
        //}
    }

    public void CloseList()
    {
        ListCall.SetActive(false);
        //ButtonCall.SetActive(false);
    }
}
