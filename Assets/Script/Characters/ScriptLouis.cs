using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem;

public class ScriptLouis : MonoBehaviour
{

    [SerializeField] GameObject ObjectMask;
    [SerializeField] GameObject TextBox;

    [SerializeField] DialogListManager dialogList;
    [SerializeField] DialogPlayer dialogPlayer;

    SceneBureauxManager sceneBureaux;

    // Start is called before the first frame update
    void Start()
    {
        dialogPlayer.SetDialog(dialogList.dialogList[0]);
        dialogPlayer.onPlayNextDialogAction += GoodSpeechFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoodSpeechFinished() //fin de dialogue
    {
        TextBox.SetActive(false);
        ObjectMask.SetActive(true);
    }

        public void ModificationTxt(int index)
    {
        /*if(sceneBureaux.PhotoIsChecked)
        {
            index = 1;
        }else
        {
            index = 0;
        }*/
    }
}
