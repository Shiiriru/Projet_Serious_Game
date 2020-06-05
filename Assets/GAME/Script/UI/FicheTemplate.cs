using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheTemplate : MonoBehaviour
{
	//public Item Infoitem;

	//[SerializeField] GameObject ButtonPhoneCall;
	//[SerializeField] GameObject ButtonDeployMap;

	[SerializeField] Button buttonAddToInventory;

	StudioEventEmitter soundEmitter;

	[SerializeField] Text textTitle;
	[SerializeField] Image imgPhoto;

	[SerializeField] Text textInfos;
	[SerializeField] RectTransform textInfosContent;

	public InfoItemBouton SoundManager;

	InventoryItemInfoObject targetInventoryItem;
	GameObject sceneInventoryItem;

	public void OpenPageObj(InventoryItemInfoObject item, bool canAddToinventory, StudioEventEmitter emitter, GameObject sceneItem = null) //(Je récupère toutes les valeurs lié à item et je récupère la valeur du bool, j'indique ici les éléments que je souhaite envoyer. Tout ce qui est entre parenthèse sont mes variables en local)
	{
		gameObject.SetActive(true);
		soundEmitter = emitter;

		if (soundEmitter != null)
		{
			try
			{ soundEmitter.Play(); }
			catch { }
		}

		targetInventoryItem = item;
		sceneInventoryItem = sceneItem;

		imgPhoto.sprite = item.image;
		imgPhoto.enabled = true;

		textTitle.text = item.name;

		buttonAddToInventory.interactable = canAddToinventory;

		imgPhoto.sprite = item.photo;
		textInfos.text = targetInventoryItem.textResume.text;
	}

	public void OnClickAddItemToInventory()
	{
		//Debug.Log("Objet" + item.name + "récolté");
		bool IsSelected = InventoryScript.instance.Add(targetInventoryItem);

		if (IsSelected)
		{
			//buttonAddToInventory.SetActive(false);
			if (sceneInventoryItem != null)
				sceneInventoryItem.SetActive(false);
			//Debug.Log("Objet" + item.name + "est détruit");
		}
	}

	public void PageArchive()
	{
		textInfos.text = targetInventoryItem.TexteWiki.text;
	}

	public void ExitFiche()
	{
		gameObject.SetActive(false);
		//ButtonPhoneCall.SetActive(false);
		//ButtonDeployMap.SetActive(false);

		if (soundEmitter != null)
		{
			try { soundEmitter.Stop(); }
			catch { }
		}
	}
}
