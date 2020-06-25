using DialogSystem;
using UnityEngine;

public class ButtonChangeScene : ButtonBase
{
	[SerializeField] SceneReference sceneRef;

	public override void OnClickButton()
	{
		base.OnClickButton();
		uiMain.ChangeScene(sceneRef.Name, 1);
	}

	public void Show(bool show)
	{
		gameObject.SetActive(show);
	}
}
