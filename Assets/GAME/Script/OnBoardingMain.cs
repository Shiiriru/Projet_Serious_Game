using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OnBoardingMain : MonoBehaviour
{
	[SerializeField] CanvasGroup photoGroup;
	[SerializeField] GameObject SpriteHappy;
	[SerializeField] GameObject SpriteSerious;

	[SerializeField] DialogGraph dialogGraph;

	[SerializeField] DatePanelInfosObject dateInfo;

	// Start is called before the first frame update
	void Start()
	{
		DialogPlayerHelper.SetDialog(dialogGraph);
	}

	public void ShowPhoto(int index)
	{
		photoGroup.DOFade(1, 1.5f);

		if (index == 0)
			SpriteHappy.SetActive(true);
		else
			SpriteSerious.SetActive(true);
	}
}
