using DialogSystem;
using UnityEngine;

public class ButtonChangeScene : ButtonBase
{
	[SerializeField] SceneReference scene;

	public override void OnClickButton()
	{
		base.OnClickButton();
		uiMain.ChangeScene(scene.Name, 1);
	}
}
