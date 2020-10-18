using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class SpotGermanTarget : MonoBehaviour
{
	RectTransform rectTransform;
	[SerializeField] Vector2[] movePaths;
	[SerializeField] Animator animator;

	int moveStep;
	bool moveRight;
	bool isKilled;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		animator = GetComponent<Animator>();

		moveStep = Random.Range(0, movePaths.Length);
		rectTransform.anchoredPosition = movePaths[moveStep];

		moveRight = Random.Range(0, 2) > 0 ? true : false;

		SetNextMoveStep();
	}

	void SetNextMoveStep()
	{
		if (isKilled)
			return;

		moveStep = moveStep + (moveRight ? 1 : -1);
		//last
		if (moveStep >= movePaths.Length)
		{
			moveRight = false;
			moveStep = movePaths.Length - 2;
		}
		//first
		if (moveStep < 0)
		{
			moveRight = true;
			moveStep = 1;
		}
		rectTransform.DOAnchorPos(movePaths[moveStep], 12f).SetEase(Ease.Linear).OnComplete(SetNextMoveStep);
	}

	public void Kill()
	{
		isKilled = false;
		DOTween.Kill(rectTransform);
		animator.SetBool("move",false);
	}
}
