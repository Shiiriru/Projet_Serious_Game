using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackJack : MiniGameBase
{
	[SerializeField] BlackJackAnimation blackJackAnim;

	[SerializeField] BlackJackCard cardPrefab;
	List<BlackJackCard> shownCardList = new List<BlackJackCard>();

	[SerializeField] Transform playerDeck;
	[SerializeField] Transform aiDeck;

	List<BlackJackCard> playerCards = new List<BlackJackCard>();
	List<BlackJackCard> aiCards = new List<BlackJackCard>();

	[SerializeField] [EventRef] string launchGameSoundPath;

	public override void Launch()
	{
		base.Launch();

		SoundPlayer.PlayOneShot(launchGameSoundPath);

		foreach (var c in shownCardList)
			c.Destroy();

		shownCardList.Clear();
		playerCards.Clear();
		aiCards.Clear();

		blackJackAnim.onAnimationFinished -= OnGameComplete;
		blackJackAnim.onAnimationFinished += OnShow;
		blackJackAnim.LaunchGame();
	}

	private void OnShow()
	{
		blackJackAnim.onAnimationFinished -= OnShow;
		StartCoroutine(LaunchGameCoroutine());
	}

	IEnumerator LaunchGameCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		//each player has 2 card
		for (int i = 0; i < 2; i++)
			for (int j = 0; j < 2; j++)
			{
				if (i == 1 && j == 1)
					AddCard(false, false, false);
				else
					AddCard(i == 0, true, false);
				yield return new WaitForSeconds(0.2f);
			}
	}

	void AddCard(bool playerAdd, bool displayToFace, bool check = true)
	{
		var card = Instantiate(cardPrefab);
		card.Init();

		shownCardList.Add(card);

		card.Show(displayToFace);
		var space = card.RectTransform.sizeDelta.x / 1.2f;
		card.transform.SetParent(playerAdd ? playerDeck : aiDeck, false);
		card.RectTransform.anchoredPosition =
			new Vector2(card.RectTransform.sizeDelta.x - space * (playerAdd ? playerCards.Count : aiCards.Count), 0);

		var cardList = playerAdd ? playerCards : aiCards;

		cardList.Add(card);
		if (check) CheckPoints(cardList);
	}

	public void OnClickHitCard()
	{
		if (gameFinished)
			return;

		AddCard(true, true);
		AIHitCard();
	}

	public void OnClickEndGame()
	{
		FnishGame(true);
	}

	int GetPoints(List<BlackJackCard> cards)
	{
		var totalPoint = 0;
		var cardACount = 0;

		foreach (var c in cards)
		{
			//add big number first
			if (c.CurrentPoint == 1)
			{
				totalPoint += 11;
				cardACount++;
			}
			else
				totalPoint += c.CurrentPoint;
		}

		for (int i = 0; i < cardACount; i++)
		{
			//check if point is too big then use 1 
			if (totalPoint > 21)
			{
				totalPoint -= 11;
				totalPoint++;
			}
		}

		return totalPoint;
	}

	//check if 1 is 11 or not
	void CheckPoints(List<BlackJackCard> cards)
	{
		var totalPoints = GetPoints(cards);
		if (totalPoints >= 21)
			FnishGame(true);
	}

	void AIHitCard()
	{
		var aiPoint = GetPoints(aiCards);
		if (aiPoint >= 21)
			return;

		var maxChance = 5f;

		if (aiPoint > 18)
			maxChance = 1.1f;
		else if (aiPoint > 15)
			maxChance = 1.3f;

		float rand = Random.Range(0, maxChance);

		if (rand > 1)
			AddCard(false, false);
	}

	public override void FnishGame(bool b)
	{
		if (gameFinished)
			return;

		StartCoroutine(EndGameCoroutine());
		gameFinished = true;
	}

	IEnumerator EndGameCoroutine()
	{
		var aiPoint = GetPoints(aiCards);
		var playerPoint = GetPoints(playerCards);

		if (playerPoint <= 21)
		{
			//ai may add card for 3 times
			for (int i = 0; i < 3; i++)
			{
				if (aiPoint < 21)
				{
					AIHitCard();
					yield return new WaitForSeconds(0.1f);
				}
			}

			aiPoint = GetPoints(aiCards);
			//over point
			if (aiPoint > 21)
				HasWon = true;
			else
				HasWon = playerPoint > aiPoint;
		}

		yield return null;
		foreach (var c in aiCards)
			c.Show(true);

		blackJackAnim.GameFinished(HasWon);
	}

	public void OnClickClose()
	{
		blackJackAnim.onAnimationFinished += OnGameComplete;
		blackJackAnim.CloseGame();
	}
}
