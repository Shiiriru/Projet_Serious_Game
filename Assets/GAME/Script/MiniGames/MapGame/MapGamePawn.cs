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
	bool isInCase;

	[SerializeField] Image image;
	[SerializeField] Color validColor;

	private void Start()
	{
		defaultPos = transform.position;
		onHoverBegin += StartMove;
		onHoverEnd += EndMove;
	}

	private void StartMove()
	{
		currentCase = null;
	}

	private void EndMove()
	{
		transform.DOMove(currentCase != null ? currentCase.transform.position : defaultPos, 0.2f).SetEase(Ease.OutQuad);
		SoundPlayer.PlayOneShot(SoundPath);
		if (CheckCorrect())
		{
			disableMove = true;
			image.color = validColor;
		}
	}

	public bool CheckCorrect()
	{
		return currentCase == targetCase;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		var touchCase = collision.GetComponent<MapGamePawnCase>();
		if (touchCase == null || currentCase == touchCase)
			return;

		currentCase = touchCase;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<MapGamePawnCase>() == null)
			return;

		currentCase = null;
	}

	public void SetPosToTargetCase()
	{
		transform.position = targetCase.transform.position;
		disableMove = true;
	}
}
