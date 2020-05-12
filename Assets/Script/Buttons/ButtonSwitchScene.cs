﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSwitchScene : ButtonBase
{
    [SerializeField] string SceneName;

    public override void OnClickButton()
    {
        base.OnClickButton();
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

}
