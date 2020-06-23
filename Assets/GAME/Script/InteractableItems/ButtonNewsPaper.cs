using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewsPaper : ButtonBase
{
	[SerializeField] NewsPaperScreen newsPaperScreen;
	[SerializeField] Sprite[] newsPaperSprites;
	public override void OnClickButton()
	{
		newsPaperScreen.Open(newsPaperSprites[GameManager.chapterCount]);
		base.OnClickButton();
	}
}
