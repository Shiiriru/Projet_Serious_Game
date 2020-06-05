using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Button))]
public class ButtonBase : MonoBehaviour
{
	public event System.Action OnChecked;

	[SerializeField] protected StudioEventEmitter soundEmitter;

	Button bouton;

	// Start is called before the first frame update
	void Awake()
	{
		bouton = GetComponent<Button>();
		bouton.onClick.AddListener(OnClickButton);
	}
	public virtual void OnClickButton()
	{
		if (OnChecked != null)
		{
			OnChecked();
		}
	}
}
