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

	[SerializeField] DialogGraph dialogGraph;

	DialogPlayer dialogPlayer;
	UIMain uiMain;

	[SerializeField] DatePanelInfosObject dateInfo;

	// Start is called before the first frame update
	void Start()
	{
		dialogPlayer = FindObjectOfType<DialogPlayer>();
		uiMain = FindObjectOfType<UIMain>();

		dialogPlayer.SetDialog(dialogGraph);
		dialogPlayer.onDialogFinished += OnboardingFinished;
	}

	private void OnboardingFinished() //fin de dialogue
	{
		PhotoCompagnie.SetActive(false);
		SpriteSerious.SetActive(false);
		SpriteHappy.SetActive(false);

		uiMain.SwitchScene("ScBureau_1", 2f, dateInfo);
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
