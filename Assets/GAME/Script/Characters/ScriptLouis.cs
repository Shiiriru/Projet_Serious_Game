using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;

public class ScriptLouis : MonoBehaviour
{

    [SerializeField] GameObject ObjectMask;
    [SerializeField] GameObject TextBox;

    [SerializeField] DialogPlayer dialogPlayer;

    SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    void Start()
    {
        //dialogPlayer.SetDialog(dialogList.dialogList[0]);
        dialogPlayer.onDialogFinished += GoodSpeechFinished;
    }

    private void GoodSpeechFinished() //fin de dialogue
    {
        TextBox.SetActive(false);
        ObjectMask.SetActive(true);
    }
}
