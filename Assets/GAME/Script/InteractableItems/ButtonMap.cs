using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMap : ButtonBase
{
    [SerializeField] GameObject MapDeployed;
    [SerializeField] GameObject ButtonDeployMap;

    [SerializeField] GameObject IconActive;
    [SerializeField] GameObject IconNoActive;

    [SerializeField] FicheTemplate ficheTemplate;
    [SerializeField] InventoryItemInfoObject Infoitem;
    [SerializeField] SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        ficheTemplate.OpenPageObj(Infoitem, false, soundEmitter);
        ButtonDeployMap.SetActive(true);
    }

    public void DeployMap()
    {
        ficheTemplate.ExitFiche();
        MapDeployed.SetActive(true);
        ButtonDeployMap.SetActive(false);
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
