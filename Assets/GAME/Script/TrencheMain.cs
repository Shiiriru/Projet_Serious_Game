using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrencheMain : MonoBehaviour
{
	[SerializeField] BlackJack blackJackGame;
	[SerializeField] CardPlayers cardPlayers;

	[SerializeField] ItemInfoObject pocketWatchItem;
	UIMain uiMain;

	private void Start()
	{
		blackJackGame.onGameFinished += BlackJackFinished;
		uiMain = FindObjectOfType<UIMain>();
	}

	public void LaunchBlackJack()
	{
		blackJackGame.LaunchGame();
	}

	private void BlackJackFinished()
	{
		cardPlayers.LaunchResultDialog();
	}

	public void LootWatch()
	{
		uiMain.UIPlayer.Inventory.Add(pocketWatchItem);
	}
}
