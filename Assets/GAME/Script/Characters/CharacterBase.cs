using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterBase : MonoBehaviour
{
	[SerializeField] protected DialogGraph dialog;
	public event System.Action onDialogFinished;

	protected virtual void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClickPlayDialog);
	}

	protected virtual void OnClickPlayDialog()
	{
		DialogPlayerHelper.SetDialog(dialog);
		DialogPlayerHelper.SetOnFinishedAction(onDialogFinished);
	}
}
