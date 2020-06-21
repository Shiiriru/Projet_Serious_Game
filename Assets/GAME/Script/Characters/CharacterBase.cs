using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterBase : MonoBehaviour
{
	[SerializeField] protected DialogGraph dialog;

	protected virtual void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClickPlayDialog);
	}

	protected virtual void OnClickPlayDialog()
	{
		DialogPlayerHelper.SetDialog(dialog);
	}
}
