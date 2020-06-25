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

	[SerializeField] [EventRef] string GazAlarmSound;
	[SerializeField] [EventRef] string GazMaskSound;


	//Son d'ambiance
	[SerializeField] [EventRef] string AmbianceTrench1;
	[SerializeField] [EventRef] string AmbianceTrench2;
	[SerializeField] [EventRef] string AmbianceTrench3;

	[SerializeField] [EventRef] string cryingSound;
	[SerializeField] [EventRef] string trenchSong;

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
				uiMain.Ambiance.Play("bg", AmbianceTrench1);
				blackJackGame.onGameComplete += BlackJackFinished;

				break;
			case 1:
				uiMain.Ambiance.Play("bg", AmbianceTrench2);
				spotGermanGame.onGameComplete += SpotGermanGameComplete;
				gasFilter.gameObject.SetActive(false);
				imageMask.gameObject.SetActive(false);

				break;
			case 2:
				uiMain.Ambiance.Play("bg", AmbianceTrench3);
				foreach (var s in soliderInjured)
					s.onDialogFinished += LaunchFinaWar;

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
		SoundPlayer.PlayOneShot(GazMaskSound);
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
		gasFilter.DOColor(new Color(gasFilter.color.r, gasFilter.color.g, gasFilter.color.b, 0), 4f);
		yield return new WaitWhile(() => DOTween.IsTweening(gasFilter));

		gasFilter.gameObject.SetActive(false);
		imageMask.gameObject.SetActive(false);

		yield return new WaitForSeconds(1.5f);
		DialogPlayerHelper.SetDialog(dialogAfterGas);
	}

	private void LaunchFinaWar()
	{
		if (!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("badgeGived"))
			return;

		foreach (var c in FindObjectsOfType<CharacterBase>())
			c.enabled = false;
		btnChangeScene.Show(false);
		StartCoroutine(FinalWarCoroutine());
	}

	IEnumerator FinalWarCoroutine()
	{
		yield return new WaitForSeconds(1f);
		uiMain.Ambiance.Play("soliderCry", cryingSound);
		yield return new WaitForSeconds(3f);
		uiMain.Ambiance.Play("song", trenchSong);

		yield return new WaitForSeconds(5f);
		uiMain.Ambiance.Stop("soliderCry", true);
		PlayNextChapterDialog();
	}
}
