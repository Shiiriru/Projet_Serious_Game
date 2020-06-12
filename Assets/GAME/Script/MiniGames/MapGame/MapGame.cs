using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGame : MiniGameBase
{
	[SerializeField] MapGamePawn[] pawnList;

	private void Start()
	{
		foreach(var p in pawnList)
			p.onHoverEnd += CheckMapCorrect;
	}

	private void CheckMapCorrect()
	{
		//return if one pawn isn't in the right place
		foreach(var p in pawnList)
		{
			if (!p.CheckCorrect())
				return;													
		}

		Debug.Log("Map game complete yeah!");
		GameComplete();

	}

	public void OnClickClose()
	{
		Show(false);
	}

	public void Show(bool show)
	{
		gameObject.SetActive(show);
	}
}
