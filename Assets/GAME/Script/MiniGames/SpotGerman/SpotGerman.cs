using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGerman : MiniGameBase
{
	public void Show()
	{
		gameObject.SetActive(true);
	}

	internal void ShootTarget()
	{
		Debug.Log("ahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
		GameComplete();

		gameObject.SetActive(false);
	}
}
