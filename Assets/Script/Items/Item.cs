using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public Sprite image = null;
    public Sprite Photo = null;
    public TextAsset TexteObject = null;
    public bool isDefaultItem = false;
    public GameObject Fichetech;

    public void Use()
    {
        Debug.Log("Using" + name);
    }
}
