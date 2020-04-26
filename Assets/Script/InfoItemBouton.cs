using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemBouton : MonoBehaviour
{
    public Item Infoitem;
    public FicheTemplate RecupInfo;

    public bool GoToInventory;

    public void LaunchTemplate()
    {
        RecupInfo.OpenPageObj(Infoitem, GoToInventory); //Je lie ce que j'avais déclaré dans ma parenthèse et je les lie avec ce que j'ai déclaré ici
    }
}
