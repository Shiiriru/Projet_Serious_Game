using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CharacterBase : MonoBehaviour
{
	[SerializeField] DialogGraph dialog;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClickPlayDialog);
	}

	void OnClickPlayDialog()
	{
		DialogPlayerHelper.SetDialog(dialog);
	}
}
