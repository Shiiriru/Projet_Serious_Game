using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemScreen : MonoBehaviour
{
	[SerializeField] Image icon;
	[SerializeField] Text textName;
	[SerializeField] [FMODUnity.EventRef] string getItemEvent;

    public void Open(Sprite iconSprite, string name)
	{
		icon.sprite = iconSprite;
		textName.text = name;

		gameObject.SetActive(true);
		SoundPlayer.PlayOneShot(getItemEvent);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
