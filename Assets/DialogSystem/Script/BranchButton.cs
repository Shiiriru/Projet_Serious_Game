using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BranchButton : MonoBehaviour
{
	Button button;
	public Button Button { get { return button; } }

	[SerializeField] Text text;

	private void Awake()
	{
		button = GetComponent<Button>();
	}

	public void Hide(bool clearFunctions)
	{
		gameObject.SetActive(false);
		if (clearFunctions)
			button.onClick.RemoveAllListeners();
	}

	public void Show(BrancheItem branche)
	{
		gameObject.SetActive(true);
		text.text = branche.text;
	}
}
