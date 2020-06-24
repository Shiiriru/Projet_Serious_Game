using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    Button Bouton;
    [SerializeField] [EventRef] string SoundPath;

    // Start is called before the first frame update
    void Start()
    {
        Bouton = GetComponent<Button>();
        Bouton.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        SoundPlayer.PlayOneShot(SoundPath);
    }
}
