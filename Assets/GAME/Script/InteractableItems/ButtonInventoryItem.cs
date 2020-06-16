using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventoryItem : ButtonBase
{
	[SerializeField] protected ItemInfoObject itemInfo;
	[SerializeField] bool disableObject = true;
	bool addedToInveneotry;

	public override void OnClickButton()
	{
		if (addedToInveneotry)
			return;

		if (disableObject)
			gameObject.SetActive(false);

		uiMain.AddToInventoryScreen(itemInfo);
		addedToInveneotry = true;

		base.OnClickButton();
	}
}
