using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemScreen : MonoBehaviour
{
	[SerializeField] Image icon;
	[SerializeField] Text textName;

    public void Open(Sprite iconSprite, string name)
	{
		icon.sprite = iconSprite;
		textName.text = name;

		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
