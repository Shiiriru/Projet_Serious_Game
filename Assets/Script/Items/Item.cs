using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject Fichetech;

    public void Use()
    {
        Fichetech.SetActive(true);
        Debug.Log("Using" + name);
    }
}
