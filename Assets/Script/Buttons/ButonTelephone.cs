using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonTelephone : ButtonBase
{
    [SerializeField] GameObject ListCall;
    [SerializeField] GameObject ButtonCall;
    [SerializeField] GameObject ButtonCallRegiment;
    [SerializeField] GameObject ButtonCallArtilery;

    [SerializeField] GameObject SoundObject;

    [SerializeField] FicheTemplate ficheTemplate;
    [SerializeField] Item Infoitem;
    [SerializeField] SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        ficheTemplate.OpenPageObj(Infoitem, false);
        ButtonCall.SetActive(true);
        SoundObject.SetActive(true);
    }

    public void CallPhone()
    {
        ficheTemplate.ExitFiche();
        ListCall.SetActive(true);
        ButtonCall.SetActive(false);

        if (sceneBureaux.MapIsChecked)
        {
            ButtonCallRegiment.SetActive(true);
        }
    }

    public void SwitchButton()
    {
        ButtonCallRegiment.SetActive(false);
        ButtonCallArtilery.SetActive(true);
    }

    public void CloseList()
    {
        ListCall.SetActive(false);
        //ButtonCall.SetActive(false);
    }
}
