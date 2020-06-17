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

		SoundPlayer.PlayOneShot(soundEvt);
		uiMain.OpenFicheTemplate(Infoitem);
		uiMain.SetFicheTemplateCustomAction(customActionSprite, DeployMap);
	}

	public void DeployMap()
	{
		mapGame.Show(true);
	}
}
