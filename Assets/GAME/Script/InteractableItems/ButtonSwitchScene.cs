using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSwitchScene : ButtonBase
{
	[SerializeField] string SceneName;
	UIMain uiMain;

	private void Start()
	{
		uiMain = FindObjectOfType<UIMain>();
	}

	public override void OnClickButton()
	{
		base.OnClickButton();
		uiMain.SwitchScene(SceneName, 1);
	}
}
