using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Button))]
public class ButtonBase : MonoBehaviour
{
	public event System.Action onCheckedAction;

	[SerializeField] [EventRef] protected string soundEvt;

	protected Button button;
	protected UIMain uiMain;

	// Start is called before the first frame update
	void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClickButton);

		uiMain = FindObjectOfType<UIMain>();
	}

	public virtual void OnClickButton()
	{
		if(!string.IsNullOrEmpty(soundEvt))
			SoundPlayer.PlayOneShot(soundEvt);
		OnChecked();
	}

	public void OnChecked()
	{
		if (onCheckedAction != null)
		{
			onCheckedAction();
		}
	}
}
