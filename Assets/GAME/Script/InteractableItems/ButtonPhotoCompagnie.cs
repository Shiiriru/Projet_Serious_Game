using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPhotoCompagnie : ButtonBase
{
    [SerializeField] GameObject PhotoCpnDeployed;
    [SerializeField] GameObject SoundObject;

    // Start is called before the first frame update
    public override void OnClickButton()
    {
        base.OnClickButton();
        PhotoCpnDeployed.SetActive(true);
        SoundObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotoCpnDeployed == true && Input.GetMouseButtonDown(0))
        {
            PhotoCpnDeployed.SetActive(false);
        }
    }
}
