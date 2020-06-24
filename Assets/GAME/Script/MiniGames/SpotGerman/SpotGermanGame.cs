using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotGermanGame : MiniGameBase
{
	[SerializeField] GameObject foundText;
	[SerializeField] SpotGermanSpot spot;

    [SerializeField] [EventRef] string SoundPath;

    [SerializeField] RectTransform ambianceRect;
	[SerializeField] RectTransform canvasRect;
	float canvasCenterX;
	float ambianceScrolloffsetX;

	private void Awake()
	{
		foundText.SetActive(false);
	}

	private void Start()
	{
		canvasCenterX = canvasRect.sizeDelta.x / 2;
		ambianceScrolloffsetX = (ambianceRect.sizeDelta.x - canvasRect.sizeDelta.x) / 2;
	}

	public override void Launch()
	{
		base.Launch();
	}

	private void LateUpdate()
	{
		if (gameFinished)
			return;

		var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		spot.transform.position = Vector2.Lerp(spot.transform.position, worldPoint, 30 * Time.deltaTime);
		ScrollMap();

		if (Input.GetMouseButtonDown(0))
			if (spot.SpotedTarget != null)
				FnishGame();
	}

	void ScrollMap()
	{
		var point = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out point);
		var scrollOffset = canvasCenterX / 3;

		//scroll left
		if (point.x < -scrollOffset)
			AmbianceScroll(true);
		//right
		else if (point.x > scrollOffset)
			AmbianceScroll(false);
		else
			ambianceRect.anchoredPosition = ambianceRect.anchoredPosition;
	}

	void AmbianceScroll(bool left)
	{
		var moveSpeed = 0;

		if ((left && ambianceRect.anchoredPosition.x < ambianceScrolloffsetX) ||
			(!left && ambianceRect.anchoredPosition.x > -ambianceScrolloffsetX))
			moveSpeed = 80;

		ambianceRect.anchoredPosition =
					Vector2.Lerp(ambianceRect.anchoredPosition, ambianceRect.anchoredPosition + (left ? Vector2.right : Vector2.left), moveSpeed * Time.deltaTime);
	}

	public override void FnishGame()
	{
		if (gameFinished)
			return;
		foundText.SetActive(true);

        SoundPlayer.PlayOneShot(SoundPath);

        spot.SpotedTarget.Kill();

		StartCoroutine(GameFinishCoroutine());
		gameFinished = true;
	}

	IEnumerator GameFinishCoroutine()
	{
		yield return new WaitForSeconds(2f);
		OnGameComplete();
		gameObject.SetActive(false);
	}
}
