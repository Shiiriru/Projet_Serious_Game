using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBase : MonoBehaviour
{
    public event System.Action OnChecked;
    public event System.Action OnCompleted;

    Button bouton;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bouton = GetComponent<Button>();
        bouton.onClick.AddListener(OnClickButton);
    }
    public virtual void OnClickButton()
    {
        if(OnChecked != null)
        {
            OnChecked();
        }
    }
}
