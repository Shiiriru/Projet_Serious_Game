using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewsPaper : ButtonBase
{
	[SerializeField] ImageZoomScreen displayScreen;
	[SerializeField] Sprite[] newsPaperSprites;
	public override void OnClickButton()
	{
		displayScreen.Open(newsPaperSprites[GameManager.chapterCount]);
		base.OnClickButton();
	}
}
