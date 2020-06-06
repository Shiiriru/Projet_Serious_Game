using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGame : MiniGameBase
{
	public void OnClickClose()
	{
		Show(false);
		GameComplete(); 
	}

	public void Show(bool show)
	{
		gameObject.SetActive(show);
	}
}
