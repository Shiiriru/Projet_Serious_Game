using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMap : ButtonBase
{
    [SerializeField] GameObject MapDeployed;
    [SerializeField] GameObject ButtonDeployMap;
    [SerializeField] GameObject SoundObject;

    [SerializeField] FicheTemplate ficheTemplate;
    [SerializeField] Item Infoitem;
    [SerializeField] SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        ficheTemplate.OpenPageObj(Infoitem, false);
        ButtonDeployMap.SetActive(true);
        SoundObject.SetActive(true);
    }

    public void DeployMap()
    {
        ficheTemplate.ExitFiche();
        MapDeployed.SetActive(true);
        ButtonDeployMap.SetActive(false);
    }

    public void CloseList()
    {
        MapDeployed.SetActive(false);
    }
}
