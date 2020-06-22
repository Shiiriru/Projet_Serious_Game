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

	[SerializeField] [EventRef] string openInventoryEvt = "";
	[SerializeField] [EventRef] string closeInventoryEvt = "";
	[SerializeField] [EventRef] string noteEvt = "";

	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		ficheTemplate.onClose += () => ShowBlackFilter(false);

		inventory.onOpen += ChangeInventoryButtonSprite;
		inventory.onClose += ChangeInventoryButtonSprite;
	}
	private void Start()
	{
		inventory.Init();
		CloseFicheTemplate();
		HidePhoto();
	}

	public void OnClickOpenInventory()
	{
		isBagOpend = !isBagOpend;
		OpenInventory(isBagOpend);
	}

	public void OpenInventory(bool b)
	{
		inventory.Show(isBagOpend);
		SoundPlayer.PlayOneShot(isBagOpend ? openInventoryEvt : closeInventoryEvt);
	}

	void ChangeInventoryButtonSprite()
	{
		imgInventory.sprite = isBagOpend ? spriteInventoryOpen : spriteInventoryClose;
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

	void ShowBlackFilter(bool show)
	{
		hideUIButton.gameObject.SetActive(show);
	}
}
