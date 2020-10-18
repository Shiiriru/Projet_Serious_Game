using DG.Tweening;
using DialogSystem;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWarMain : MonoBehaviour
{
	UIMain uiMain;
	[SerializeField] DialogGraph dialogFinalWar;

	private void Awake()
	{
		uiMain = FindObjectOfType<UIMain>();
		uiMain.onChangeSceneFinished += Init;
	}

	private void Init()
	{
		DialogPlayerHelper.SetDialog(dialogFinalWar);
	}
}
