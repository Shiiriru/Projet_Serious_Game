using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewItemInfo", menuName = "Item Info")]
public class ItemInfoObject : ScriptableObject
{
	public string Id = "";
	public string name = "New item";
	public InventoryItemType itemType;

	public Sprite imageInventory;
	[FormerlySerializedAs("image")] public Sprite imageResume = null;
	[FormerlySerializedAs("photo")] public Sprite imageWIKI = null;

	[FormerlySerializedAs("TexteObject")] public TextAsset textResume = null;
	public TextAsset TexteWiki = null;
}


public enum InventoryItemType
{
	Other,
	InfoItem,
	Photo,
}