using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPhoto : ButtonBase
{
	UIPlayer uiPlayer;
	[SerializeField] Sprite spritePhoto;

	private void Start()
	{
		uiPlayer = FindObjectOfType<UIPlayer>();
		if (spritePhoto == null)
			spritePhoto = button.image.sprite;
	}

	// Start is called before the first frame update
	public override void OnClickButton()
	{
		base.OnClickButton();
		uiPlayer.ShowPhoto(spritePhoto);
	}
}
