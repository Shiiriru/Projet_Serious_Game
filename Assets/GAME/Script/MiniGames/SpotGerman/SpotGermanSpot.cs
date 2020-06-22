using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGermanSpot : MonoBehaviour
{
	public SpotGermanTarget SpotedTarget { get; private set; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<SpotGermanTarget>() != null)
			SpotedTarget = collision.GetComponent<SpotGermanTarget>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<SpotGermanTarget>() != null)
			SpotedTarget = null;
	}
}
