using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMap : InfoItemBouton
{
	[SerializeField] MapGame mapGame;

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		OnChecked();

		uiMain.OpenFicheTemplate(Infoitem, soundEmitter);
		uiMain.SetFicheTemplateCustomAction(customActionSprite, DeployMap);
		//ButtonDeployMap.SetActive(true);
	}

	public void DeployMap()
	{
		//uiMain.CloseFicheTemplate();
		mapGame.Show(true);
	}
}
