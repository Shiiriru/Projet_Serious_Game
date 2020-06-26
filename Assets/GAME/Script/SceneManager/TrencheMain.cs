using DG.Tweening;
using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrencheMain : SceneManagerBase
{
	[SerializeField] ButtonChangeScene btnChangeScene;

	[SerializeField] DialogGraph[] dialogEndChapter;

	[SerializeField] BlackJack blackJackGame;
	[SerializeField] CardPlayers cardPlayers;
	[SerializeField] ItemInfoObject pocketWatchItem;
	[SerializeField] ItemInfoObject maskItem;

	//chapter 2
	[SerializeField] SpotGermanGame spotGermanGame;
	[SerializeField] ScriptLouis spotGermanLouis;
	[SerializeField] DialogGraph dialogAfterGas;
	[SerializeField] Image gasFilter;
	[SerializeField] Image imageMask;
	[SerializeField] Sprite oldMaskSprite;

	//chapter 3
	[SerializeField] CharacterBase[] soliderInjured;
	[SerializeField] DialogGraph dialogSoliderSinging;

	[SerializeField] [EventRef] string GazAlarmSound;

	[SerializeField] [EventRef] string cryingSound;
	[SerializeField] [EventRef] string trenchSong;

	//Son d'ambiance
	[SerializeField] [EventRef] string AmbianceTrench1;
	[SerializeField] [EventRef] string AmbianceTrench2;
	[SerializeField] [EventRef] string AmbianceTrench3;


	protected override void Start()
	{
		base.Start();
		uiMain.onChangeSceneFinished += () => StartCoroutine(CheckChapterFinishCoroutine());
		btnChangeScene.Show(true);
	}

	protected override void SetupChapters()
	{
		base.SetupChapters();

		switch (GameManager.chapterCount)
		{
			case 0:
				SoundPlayer.PlayEvent("bg", AmbianceTrench1);
				blackJackGame.onGameComplete += BlackJackFinished;

				break;
			case 1:
				SoundPlayer.PlayEvent("bg", AmbianceTrench2);
				spotGermanGame.onGameComplete += SpotGermanGameComplete;
				gasFilter.gameObject.SetActive(false);
				imageMask.gameObject.SetActive(false);

				break;
			case 2:
				SoundPlayer.PlayEvent("bg", AmbianceTrench3);
				foreach (var s in soliderInjured)
					s.onDialogFinished += SoliderSinging;
				break;
		}
	}

	IEnumerator CheckChapterFinishCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(2f);

			if (!uiMain.IsChangingScene && !DialogPlayerHelper.IsPlaying() &&
				(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isTalkedToLouis"))
				switch (GameManager.chapterCount)
				{
					case 0:
						if ((bool)DialogPlayerHelper.VariableSourceMgr.GetValue("MapChecked") &&
							(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("letterSent"))
						{
							PlayNextChapterDialog();
							yield break;
						}
						break;
					case 1:
						if ((bool)DialogPlayerHelper.VariableSourceMgr.GetValue("spotGermanFinished"))
						{
							LaunchGasEvent();
							PlayNextChapterDialog();
							yield break;
						}
						break;
					case 2:
						yield break;
				}
		}
	}

	void PlayNextChapterDialog()
	{
		DialogPlayerHelper.SetDialog(dialogEndChapter[GameManager.chapterCount]);
		GameManager.chapterCount += 1;
	}

	void LaunchGasEvent()
	{
		SoundPlayer.PlayOneShot(GazAlarmSound);
		gasFilter.gameObject.SetActive(true);
		gasFilter.DOColor(new Color(gasFilter.color.r, gasFilter.color.g, gasFilter.color.b, 1), 7f);
	}

	public void LaunchBlackJack()
	{
		blackJackGame.Launch();
	}

	private void BlackJackFinished()
	{
		cardPlayers.LaunchResultDialog();
	}

	public void LaunchSpotGerman()
	{
		spotGermanGame.Launch();
	}

	private void SpotGermanGameComplete()
	{
		spotGermanLouis.SpotFinish();
	}

	public void LootWatch()
	{
		uiMain.UIPlayer.Inventory.Add(pocketWatchItem);
	}

	public void LootMask()
	{
		uiMain.UIPlayer.Inventory.Add(maskItem);
	}

	public void WearMask(bool newMask)
	{
		if (!newMask)
			imageMask.sprite = oldMaskSprite;
		imageMask.gameObject.SetActive(true);
	}

	public void AfterGasAttack()
	{
		//hide all characteres
		foreach (var c in FindObjectsOfType<CharacterBase>())
			c.gameObject.SetActive(false);
		btnChangeScene.Show(false);

		StartCoroutine(AfertGazAttackCoroutine());
	}

	IEnumerator AfertGazAttackCoroutine()
	{
		gasFilter.DOColor(new Color(gasFilter.color.r, gasFilter.color.g, gasFilter.color.b, 0), 6f).SetEase(Ease.Linear);
		yield return new WaitWhile(() => DOTween.IsTweening(gasFilter));

		gasFilter.gameObject.SetActive(false);
		imageMask.gameObject.SetActive(false);
		SoundPlayer.StopEvent("mask", true);

		yield return new WaitForSeconds(1.5f);
		DialogPlayerHelper.SetDialog(dialogAfterGas);
	}

	private void SoliderSinging()
	{
		if (!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("badgeGived"))
			return;

		foreach (var c in FindObjectsOfType<CharacterBase>())
			c.GetComponent<Image>().raycastTarget = false;
		btnChangeScene.Show(false);
		StartCoroutine(SoliderSingingCoroutine());
	}

	IEnumerator SoliderSingingCoroutine()
	{
		yield return new WaitForSeconds(1f);
		SoundPlayer.PlayEvent("soliderCry", cryingSound);
		yield return new WaitForSeconds(8f);
		DialogPlayerHelper.SetDialog(dialogSoliderSinging);
	}

	public void LaunchFinalWar()
	{
		StartCoroutine(FinalWarCoroutine());
	}

	IEnumerator FinalWarCoroutine()
	{
		SoundPlayer.PlayEvent("song", trenchSong);
		SoundPlayer.StopEvent("soliderCry", true);
		yield return new WaitForSeconds(10f);
		PlayNextChapterDialog();
	}
}
