using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPhoto : ButtonBase
{
	UIPlayer uiPlayer;

	private void Start()
	{
		uiPlayer = FindObjectOfType<UIPlayer>();
	}

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		base.OnClickButton();
		uiPlayer.ShowPhoto(button.image.sprite);
	}
}
