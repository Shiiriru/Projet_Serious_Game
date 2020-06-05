using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName ="NewInventoryItem", menuName = "Inventory Item Info")]
public class InventoryItemInfoObject : ScriptableObject
{
	public string Id = "";
	public string name = "New item";
	[FormerlySerializedAs("icon")] public Sprite imageInventory = null;
	[FormerlySerializedAs("image")] public Sprite imageResume = null;
	[FormerlySerializedAs("photo")] public Sprite imageWIKI = null;

	[FormerlySerializedAs("TexteObject")] public TextAsset textResume = null;
    public TextAsset TexteWiki = null;
}
