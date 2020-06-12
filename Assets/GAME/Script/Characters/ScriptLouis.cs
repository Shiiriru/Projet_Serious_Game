using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;

public class ScriptLouis : CharacterBase
{
	protected override void OnClickPlayDialog()
	{
		DialogPlayerHelper.SetOnFinishedAction(() => gameObject.SetActive(false));
		base.OnClickPlayDialog();
	}
}
