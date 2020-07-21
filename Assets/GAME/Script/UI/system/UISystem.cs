using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
	public enum Page
	{
		Options,
	}
	[SerializeField] Text titleText;

	GameObject lastOpenedPage;
	[SerializeField] GameObject optionPage;

	public void Open(Page page)
	{
		if (lastOpenedPage != null)
			lastOpenedPage.SetActive(false);
		switch (page)
		{
			case Page.Options:
				titleText.text = "OPTIONS";
				lastOpenedPage = optionPage;
				break;
		}

		lastOpenedPage.SetActive(true);
		gameObject.SetActive(true);
	}

	public void OnClickClose()
	{
		gameObject.SetActive(false);
	}
}
