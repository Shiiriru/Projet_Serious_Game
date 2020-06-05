using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIInventory : MonoBehaviour
{
	[SerializeField] GameObject SacDeployed;
	bool isBagOpend;

	[SerializeField] [EventRef] string SoundOpenInventory;
	[SerializeField] [EventRef] string soundCloseInventory;
	[SerializeField] [EventRef] string soundOpenNote;

	Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void OnClickOpenInventory()
	{
		isBagOpend = !isBagOpend;
		OpenInventory(isBagOpend);
	}

	public void OpenInventory(bool b)
	{
		SacDeployed.SetActive(isBagOpend);

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
	}

	void PlaySound(string path)
	{
		FMODUnity.RuntimeManager.PlayOneShot(path);
	}
}
