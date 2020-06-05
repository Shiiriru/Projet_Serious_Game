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
	[SerializeField] VariableSourceManager variableSourceManager;

	// Start is called before the first frame update
	void Start()
	{
		dialogPlayer = FindObjectOfType<DialogPlayer>();

		dialogPlayer.SetDialog(dialogGraph);
		dialogPlayer.onDialogFinished += OnboardingFinished;

		dialogPlayer.SetVariableManger(variableSourceManager);
	}

	private void OnboardingFinished() //fin de dialogue
	{
		PhotoCompagnie.SetActive(false);
		SpriteSerious.SetActive(false);
		SpriteHappy.SetActive(false);

		dialogPlayer.DatePanel.Launch(2f);
		dialogPlayer.DatePanel.onDisplayFinished += SwitchScene;
	}

	private void SwitchScene()
	{
		SceneManager.LoadScene("ScBureau_1", LoadSceneMode.Single);
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
