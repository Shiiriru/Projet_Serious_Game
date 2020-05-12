using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemBouton : ButtonBase
{
    public Item Infoitem;
    public FicheTemplate ficheTemplate;

    public bool GoToInventory;

    public override void OnClickButton()
    {
        base.OnClickButton(); //fonction basique + fonction particulière
        ficheTemplate.OpenPageObj(Infoitem, GoToInventory); //Je lie ce que j'avais déclaré dans ma parenthèse et je les lie avec ce que j'ai déclaré ici
    }
}
