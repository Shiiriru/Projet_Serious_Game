using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPhotoCompany : ButtonPhoto
{
	[SerializeField] Sprite spriteSerious;

	protected override void Init()
	{
		bool isSmile = (bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isPhotoSmile");
		if (!isSmile)
			button.image.sprite = spriteSerious;

		base.Init();
	}
}
