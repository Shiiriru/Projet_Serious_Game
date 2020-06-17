using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIPlayer : MonoBehaviour
{
	[SerializeField] InventoryScript inventory;
	public InventoryScript Inventory { get { return inventory; } }

	[SerializeField] Image imgInventory;
	[SerializeField] Sprite spriteInventoryOpen;
	[SerializeField] Sprite spriteInventoryClose;

	bool isBagOpend;
	[SerializeField] Button hideUIButton;
	[SerializeField] Image photoContent;

	[SerializeField] FicheTemplate ficheTemplate;

	[SerializeField] GetItemScreen getItemScreen;

	[SerializeField] [EventRef] string openInventoryEvt = "";
	[SerializeField] [EventRef] string closeInventoryEvt = "";
	[SerializeField] [EventRef] string putInInventoryEvt = "";
	[SerializeField] [EventRef] string noteEvt = "";

	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		inventory.Init();
		CloseFicheTemplate();
		HidePhoto();
		OnClickCloseGetItemScreen();

		ficheTemplate.onClose += () => ShowBlackFilter(false);
	}

	public void OnClickOpenInventory()
	{
		isBagOpend = !isBagOpend;
		OpenInventory(isBagOpend);
	}

	public void OpenInventory(bool b)
	{
		imgInventory.sprite = b ? spriteInventoryOpen : spriteInventoryClose;

		inventory.Show(isBagOpend);
		SoundPlayer.PlayOneShot(isBagOpend ? openInventoryEvt : closeInventoryEvt);
	}

	//public void DeployObjectNote()
	//{
	//	NoteDeployed.SetActive(true);
	//	BoutonNoteOuverture.SetActive(false);

	//	PlaySound(soundOpenNote);

	//	if (SacDeployed == true)
	//	{
	//		SacDeployed.SetActive(false);
	//		BoutonSacOuverture.SetActive(true);
	//	}
	//}

	public void Show(bool isShow)
	{
		animator.SetBool("show", isShow);
		if (!isShow)
		{
			inventory.Show(isShow);
			CloseFicheTemplate();
			HidePhoto();
		}
	}

	public void OpenFicheTemplate(ItemInfoObject item)
	{
		ShowBlackFilter(true);
		ficheTemplate.Open(item);
	}

	public void SetFicheTemplateCustomAction(Sprite sprite, System.Action action)
	{
		ficheTemplate.SetCustomAction(sprite, action);
	}

	public void CloseFicheTemplate()
	{
		ShowBlackFilter(false);
		ficheTemplate.Close();
	}

	public void ShowPhoto(Sprite sprite)
	{
		ShowBlackFilter(true);
		hideUIButton.onClick.AddListener(HidePhoto);

		photoContent.sprite = sprite;
		photoContent.gameObject.SetActive(true);
	}

	public void HidePhoto()
	{
		ShowBlackFilter(false);
		hideUIButton.onClick.RemoveAllListeners();

		photoContent.gameObject.SetActive(false);
	}

	public void AddItemToInventory(ItemInfoObject itemInfo)
	{
		getItemScreen.Open(itemInfo.imageInventory, itemInfo.name);
		ShowBlackFilter(true);
		inventory.Add(itemInfo);

		SoundPlayer.PlayOneShot(putInInventoryEvt);
	}

	public void OnClickCloseGetItemScreen()
	{
		getItemScreen.Close();
		ShowBlackFilter(false);
	}

	void ShowBlackFilter(bool show)
	{
		hideUIButton.gameObject.SetActive(show);
	}
}
