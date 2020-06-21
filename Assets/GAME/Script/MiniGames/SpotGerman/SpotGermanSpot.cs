using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotGermanSpot : MonoBehaviour
{
	[SerializeField] SpotGermanGame gameMain;
	SpotGermanTarget targetSpoted;
	private void Update()
	{
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, 30 * Time.deltaTime);

		if (Input.GetMouseButtonDown(0))
			if (targetSpoted != null)
				gameMain.FnishGame();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<SpotGermanTarget>() != null)
			targetSpoted = collision.GetComponent<SpotGermanTarget>();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<SpotGermanTarget>() != null)
			targetSpoted = null;
	}
}
