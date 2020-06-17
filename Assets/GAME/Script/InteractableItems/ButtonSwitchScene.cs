using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSwitchScene : ButtonBase
{
	[SerializeField] SceneAsset scene;

	public override void OnClickButton()
	{
		base.OnClickButton();
		uiMain.ChangeScene(scene.name, 1);
	}
}
