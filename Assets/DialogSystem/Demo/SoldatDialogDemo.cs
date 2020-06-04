using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldatDialogDemo : MonoBehaviour
{
	[SerializeField] DialogListManager dialogListMgr;
	[SerializeField] DialogPlayer dialogPlayer;

	public void OnClickPlayDialog()
	{
		dialogPlayer.SetDialog(dialogListMgr.dialogList[0]);
		dialogPlayer.VariableSourceMgr.enabled = true;
	}
}
