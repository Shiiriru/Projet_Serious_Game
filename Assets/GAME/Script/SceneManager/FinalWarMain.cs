using DG.Tweening;
using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWarMain : MonoBehaviour
{
	UIMain uiMain;

	[SerializeField] DialogGraph dialogFinalWar;
	[SerializeField] DialogGraph dialogAfterWar;

    [SerializeField] [EventRef] string FinalWarAmbiance;
    [SerializeField] [EventRef] string AfterWarAmbiance;
    [SerializeField] [EventRef] string FinalHeadShotSound;

    [SerializeField] CanvasGroup gameEndTextGroup;
	[SerializeField] SceneReference sceneMainMenu;
	private void Start()
	{
		uiMain = FindObjectOfType<UIMain>();

		DialogPlayerHelper.SetDialog(dialogFinalWar);
		DialogPlayerHelper.SetOnFinishedAction(PlayAfterWarDialog);

        //uiMain.Ambiance.Play(FinalWarAmbiance);
    }

	void PlayAfterWarDialog()
	{
		StartCoroutine(AfterWarCoroutine());

        SoundPlayer.PlayOneShot(FinalHeadShotSound);
        //uiMain.Ambiance.Stop(true);
    }

	IEnumerator AfterWarCoroutine()
	{
		yield return new WaitForSeconds(1f);
		DialogPlayerHelper.SetDialog(dialogAfterWar);
		DialogPlayerHelper.SetOnFinishedAction(EndGame);

        //uiMain.Ambiance.Play(AfterWarAmbiance);
    }

	void EndGame()
	{
		StartCoroutine(EndGameCorutine());
	}

	IEnumerator EndGameCorutine()
	{
		yield return new WaitForSeconds(2f);
		gameEndTextGroup.DOFade(1, 3f);
		yield return new WaitForSeconds(5f);
		gameEndTextGroup.DOFade(0, 2f);
		yield return new WaitForSeconds(4f);
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
		SceneManager.LoadScene(sceneMainMenu, LoadSceneMode.Single);

        //uiMain.Ambiance.Stop(true);
    }
}
