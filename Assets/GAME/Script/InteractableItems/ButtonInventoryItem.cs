using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventoryItem : ButtonBase
{
	[SerializeField] protected ItemInfoObject itemInfo;
	[SerializeField] bool disableObject = true;
	bool addedToInveneotry;

	private void Start()
	{
		//check if object is already added to inventory
		if (uiMain.UIPlayer.Inventory.AlreadyPicked(itemInfo))
			gameObject.SetActive(false);
	}

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
