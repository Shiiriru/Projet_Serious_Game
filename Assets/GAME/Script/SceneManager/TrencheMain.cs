using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrencheMain : SceneManagerBase
{
	[SerializeField] DialogGraph[] dialogEndChapter;

    [SerializeField] [EventRef] string SoundPath;

    [SerializeField] BlackJack blackJackGame;
	[SerializeField] CardPlayers cardPlayers;
	[SerializeField] ItemInfoObject pocketWatchItem;
	[SerializeField] ItemInfoObject maskItem;

	//chapter 2
	[SerializeField] SpotGermanGame spotGermanGame;
	[SerializeField] ScriptLouis spotGermanLouis;
	[SerializeField] DialogGraph dialogAfterGas;

	//chapter 3
	[SerializeField] CharacterBase soliderHurt;

	protected override void Start()
	{
		base.Start();
		uiMain.onChangeSceneFinished += () => StartCoroutine(CheckChapterFinishCoroutine());
	}

	protected override void SetupChapters()
	{
		base.SetupChapters();

		switch (GameManager.chapterCount)
		{
			case 0:
				blackJackGame.onGameComplete += BlackJackFinished;

				break;
			case 1:
				spotGermanGame.onGameComplete += SpotGermanGameComplete;
				break;
			case 2:
				soliderHurt.onDialogFinished += LaunchFinaWar;
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
							NextChapter();
							yield break;
						}
						break;
					case 1:
						if ((bool)DialogPlayerHelper.VariableSourceMgr.GetValue("spotGermanFinished"))
						{
                            SoundPlayer.PlayOneShot(SoundPath);

                            NextChapter();
							yield break;
						}
						break;
					case 2:
						yield break;
				}
		}
	}

	void NextChapter()
	{
		DialogPlayerHelper.SetDialog(dialogEndChapter[GameManager.chapterCount]);
		GameManager.chapterCount += 1;
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

	 public void AfterGasAttack()
	{
		DialogPlayerHelper.SetDialog(dialogAfterGas);
	}

	private void LaunchFinaWar()
	{
		if (!(bool)DialogPlayerHelper.VariableSourceMgr.GetValue("isTalkedToLouis"))
			return;
		NextChapter();
	}
}
