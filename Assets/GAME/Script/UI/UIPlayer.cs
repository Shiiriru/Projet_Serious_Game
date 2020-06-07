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

	bool isBagOpend;
	[SerializeField] Button hideUIButton;
	[SerializeField] Image photoContent;

	[SerializeField] FicheTemplate ficheTemplate;

	[SerializeField] [EventRef] string SoundOpenInventory;
	[SerializeField] [EventRef] string soundCloseInventory;
	[SerializeField] [EventRef] string soundOpenNote;

	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		inventory.Init();
		CloseFicheTemplate();
		HidePhoto();

		ficheTemplate.onClose += () => ShowBlackFilter(false);
	}

	public void OnClickOpenInventory()
	{
		isBagOpend = !isBagOpend;
		OpenInventory(isBagOpend);
	}

	public void OpenInventory(bool b)
	{
		inventory.Show(isBagOpend);
		PlaySound(isBagOpend ? SoundOpenInventory : soundCloseInventory);
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

	void PlaySound(string path)
	{
		FMODUnity.RuntimeManager.PlayOneShot(path);
	}

	public void OpenFicheTemplate(InventoryItemInfoObject item, InfoItemBouton sceneItem, bool canAddToinventory,
	StudioEventEmitter emitter = null, Dictionary<string, System.Action> customActions = null)
	{
		ShowBlackFilter(true);
		ficheTemplate.Open(item, sceneItem, canAddToinventory, emitter, customActions);
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
