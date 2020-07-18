using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class MapGamePawn : DragableButton
{
	Vector3 defaultPos;

	[SerializeField] [EventRef] string SoundPath;

	[SerializeField] MapGamePawnCase targetCase;
	MapGamePawnCase currentCase;
	public bool isCorrect { get; private set; }


	[SerializeField] Image image;
	[SerializeField] Color validColor;

	MapGame mapGame;

	private void Start()
	{
		defaultPos = transform.position;
		onHoverBegin += StartMove;
		onHoverEnd += EndMove;
	}

	public void Init(MapGame _mapGame)
	{
		mapGame = _mapGame;
	}

	private void StartMove()
	{
		if (currentCase != null)
			currentCase.fillCase(null);
		currentCase = null;

		transform.SetAsLastSibling();
	}

	private void EndMove()
	{
		transform.DOMove(currentCase != null ? currentCase.transform.position : defaultPos, 0.2f).SetEase(Ease.OutQuad);
		SoundPlayer.PlayOneShot(SoundPath);

		if (currentCase != null)
		{
			currentCase.fillCase(this);
			if (currentCase == targetCase)
			{
				CorrectAnswer();
				mapGame.CheckMapCorrect();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isCorrect)
			return;

		var touchCase = collision.GetComponent<MapGamePawnCase>();
		if (touchCase == null || touchCase.targetPawn != null)
			return;

		currentCase = touchCase;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isCorrect)
			return;

		var touchCase = collision.GetComponent<MapGamePawnCase>();
		if (touchCase == null || currentCase != touchCase)
			return;

		currentCase = null;
	}

	public void SetPosToTargetCase()
	{
		transform.position = targetCase.transform.position;
		CorrectAnswer();
	}

	 void CorrectAnswer()
	{
		isCorrect = disableMove = true;
		GetComponent<Collider2D>().enabled = false;
		image.color = validColor;
	}
}
