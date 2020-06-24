using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OnBoardingMain : SceneManagerBase
{
	[SerializeField] Image imgPhoto;
	[SerializeField] Sprite SpriteSerious;

	[SerializeField] DialogGraph dialogGraph;

	// Start is called before the first frame update
	protected override void Start()
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
