using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="NewInventoryItem", menuName = "Inventory Item Info")]
public class InventoryItemInfoObject : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public Sprite image = null;
    public Sprite photo = null;

	[FormerlySerializedAs("TexteObject")] public TextAsset textResume = null;
    public TextAsset TexteWiki = null;
    public bool isDefaultItem = false;
}
