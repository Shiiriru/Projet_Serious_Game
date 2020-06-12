using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapGamePawn : DragableButton
{
	Vector3 defaultPos;

	[SerializeField] MapGamePawnCase targetCase;
	MapGamePawnCase currentCase;
	bool isInCase;

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
	}

	public bool CheckCorrect()
	{
		return currentCase == targetCase;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<MapGamePawnCase>() == null)
			return;

		currentCase = collision.GetComponent<MapGamePawnCase>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<MapGamePawnCase>() == null)
			return;

		currentCase = null;
	}
}
