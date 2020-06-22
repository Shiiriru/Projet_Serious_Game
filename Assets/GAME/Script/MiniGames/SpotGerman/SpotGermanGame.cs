using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotGermanGame : MiniGameBase
{
	[SerializeField] GameObject foundText;
	[SerializeField] SpotGermanSpot spot;

	[SerializeField] Image ambiance;
	[SerializeField] RectTransform canvasRect;
	float canvasCenterX;

	private void Awake()
	{
		foundText.SetActive(false);
		canvasCenterX = canvasRect.sizeDelta.x / 2;
	}

	public override void Launch()
	{
		base.Launch();
	}

	private void Update()
	{
		if (gameFinished)
			return;

		var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		spot.transform.position = Vector2.Lerp(spot.transform.position, worldPoint, 30 * Time.deltaTime);
		ScrollMap();

		if (Input.GetMouseButtonDown(0))
			if (spot.TargetSpoted != null)
				FnishGame();
	}

	void ScrollMap()
	{
		var point = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out point);
		var scrollOffset = canvasCenterX / 3;
		//scroll left
		if (point.x < scrollOffset)		
			AmbianceScroll(true);
		//right
		else if (point.x > canvasCenterX + scrollOffset)
			AmbianceScroll(false);
	}

	void AmbianceScroll(bool left)
	{
		
	}

	public override void FnishGame()
	{
		if (gameFinished)
			return;
		foundText.SetActive(true);

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
