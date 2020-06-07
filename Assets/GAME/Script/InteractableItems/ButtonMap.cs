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

		var actions = new Dictionary<string, System.Action>();
		actions.Add("Déployer map", DeployMap);
		uiMain.OpenFicheTemplate(Infoitem, this, false, soundEmitter, customActions: actions);
		//ButtonDeployMap.SetActive(true);
	}

	public void DeployMap()
	{
		//uiMain.CloseFicheTemplate();
		mapGame.Show(true);
	}
}
