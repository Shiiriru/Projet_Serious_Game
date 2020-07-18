using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWarReport : ButtonBase
{
	[SerializeField] ZoomImageScreen displayScreen;
	[SerializeField] Sprite sprite;
	public override void OnClickButton()
	{
		displayScreen.Open(sprite);
		base.OnClickButton();
	}
}
