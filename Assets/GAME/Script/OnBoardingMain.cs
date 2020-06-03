using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;
using UnityEngine.SceneManagement;

public class OnBoardingMain : MonoBehaviour
{
	[SerializeField] GameObject PhotoCompagnie;
	[SerializeField] GameObject SpriteHappy;
	[SerializeField] GameObject SpriteSerious;

	DialogPlayer dialogPlayer;
	DialogListManager dialogList;

	// Start is called before the first frame update
	void Start()
	{
		dialogPlayer = FindObjectOfType<DialogPlayer>();
		dialogList = GetComponent<DialogListManager>();

		dialogPlayer.SetDialog(dialogList.dialogList[0]);
		dialogPlayer.onDialogFinished += OnboardingFinished;
	}

	private void OnboardingFinished() //fin de dialogue
	{
		PhotoCompagnie.SetActive(false);
		SpriteSerious.SetActive(false);
		SpriteHappy.SetActive(false);

		SceneManager.LoadScene("ScBureau_1", LoadSceneMode.Single);
		//StartCoroutine(LaunchPanelInfo());
		/*if (FinPanelDate == true)
        {
            Debug.Log("papon");
            StartCoroutine(LaunchEndOnboarding());
        }*/
	}

	public void afficherPhoto(int index)
	{
		PhotoCompagnie.SetActive(true);

		if (index == 0)
			SpriteHappy.SetActive(true);	
		else		
			SpriteSerious.SetActive(true);		
	}
}
