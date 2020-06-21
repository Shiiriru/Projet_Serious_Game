using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerBase : MonoBehaviour
{
	[SerializeField] protected GameObject[] chapterObjects;
	protected UIMain uiMain;

	private void Awake()
	{
		uiMain = FindObjectOfType<UIMain>();
	}

	protected virtual void Start()
	{
		SetupChapters();
	}

	protected virtual void SetupChapters()
	{
		var chapter = GameManager.chapterCount;
		foreach (var c in chapterObjects)
			c.SetActive(false);

		chapterObjects[chapter].SetActive(true);
	}
}
