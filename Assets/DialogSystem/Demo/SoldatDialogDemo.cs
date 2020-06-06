using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldatDialogDemo : MonoBehaviour
{
	[SerializeField] DialogGraph targetDialog;
	[SerializeField] DialogPlayer dialogPlayer;

	[SerializeField] DatePanelInfosObject datePanelInfo;
	[SerializeField] DatePanel datePanel;

	public void OnClickPlayDialog()
	{
		dialogPlayer.SetDialog(targetDialog);
	}
}
