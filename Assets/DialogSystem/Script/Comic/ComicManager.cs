using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicManager : MonoBehaviour
{
	List<ComicController> controllers = new List<ComicController>();

	public void Clear()
	{
		foreach (var c in controllers)
		{
			Destroy(c.gameObject);
		}
		controllers.Clear();
	}

	public void ShowComic(ComicController prefab, int index, float duration)
	{
		bool prefabExist = false;
		ComicController controller = null;

		foreach (var c in controllers)
		{
			if (c.gameObject.name == prefab.gameObject.name)
			{
				controller = c.GetComponent<ComicController>();
				prefabExist = true;
				break;
			}
		}

		//instantiate prefab if isn't existe
		if (!prefabExist)
		{
			controller = Instantiate<ComicController>(prefab);
			controller.gameObject.name = prefab.gameObject.name;
			controller.Init(transform);

			controllers.Add(controller);
		}

		controller.ShowComicObject(index, duration);
	}
}
