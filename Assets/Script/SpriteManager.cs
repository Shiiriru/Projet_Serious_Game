using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{

    public static SpriteManager Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public Sprite MasqueSprite;
    public Sprite PhotoSprite;
    public Sprite JournalSprite;
    public Sprite MapSprite;

}
