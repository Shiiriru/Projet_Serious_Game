using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMap : InfoItemBouton
{
    [SerializeField] GameObject MapDeployed;
    //[SerializeField] GameObject ButtonDeployMap;

    [SerializeField] GameObject IconActive;
    [SerializeField] GameObject IconNoActive;

    [SerializeField] SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        uiMain.OpenFicheTemplate(Infoitem, false, soundEmitter);
        //ButtonDeployMap.SetActive(true);
    }

    public void DeployMap()
    {
        uiMain.CloseFicheTemplate();
        MapDeployed.SetActive(true);
        //ButtonDeployMap.SetActive(false);
    }

    public void SwitchMap()
    {
        IconActive.SetActive(false);
        IconNoActive.SetActive(true);
    }

    public void CloseList()
    {
        MapDeployed.SetActive(false);
    }
}
