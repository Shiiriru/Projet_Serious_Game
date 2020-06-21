using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OnBoardingMain : MonoBehaviour
{
	[SerializeField] Image imgPhoto;
	[SerializeField] Sprite SpriteSerious;

	[SerializeField] DialogGraph dialogGraph;

	[SerializeField] DatePanelInfosObject dateInfo;

	// Start is called before the first frame update
	void Start()
	{
		DialogPlayerHelper.SetDialog(dialogGraph);
	}

	public void ShowPhoto(bool smile)
	{
		if (!smile)
			imgPhoto.sprite = SpriteSerious;
		imgPhoto.DOColor(Color.white, 1.5f);
	}
}
