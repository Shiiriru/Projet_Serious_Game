using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
	[CreateAssetMenu(fileName = "DialogGroup", menuName = "Dialog/Create new group", order = 1)]
	public class DialogGroupItem : ScriptableObject
	{
		public string id;
		public VariableSourceObject variableSource;

		public List<VariableObject> activeConditions = new List<VariableObject>();

		[System.Obsolete("This is an obsolete list, it 'll be removed soon")]
		public List<DialogItem> dialogList = new List<DialogItem>();

		public List<DialogItemBase> itemList = new List<DialogItemBase>();


		private void OnEnable()
		{
			if (string.IsNullOrEmpty(id))
				id = name;
		}

		public void AddOldDialogsToList()
		{
			foreach (var d in dialogList)
				AddItemToList(d);
		}

		public void AddItemToList(DialogItemBase item)
		{
			itemList.Add(item);
			item.index = itemList.Count - 1;
		}

		public void InsertItemToList(DialogItemBase item, int index)
		{
			if (index > itemList.Count - 1)
			{
				AddItemToList(item);
				return;
			}

			itemList.Insert(index, item);

			RefreshListIndex();
		}

		public void SwapItems(int index1, int index2)
		{
			if (index1 < 0 || index2 < 0 || index1 > itemList.Count - 1 || index2 > itemList.Count - 1)
				return;

			var item1 = itemList[index1];
			var item2 = itemList[index2];
			item1.index = index2;
			item2.index = index1;

			itemList[item1.index] = item1;
			itemList[item2.index] = item2;
		}

		public DialogItemBase CreateItem(Type type)
		{
			try
			{
				var item = CreateInstance(type) as DialogItemBase;
				item.targetGroup = this;
				return item;
			}
			catch
			{ return null; }
		}

		public void RemoveItem(DialogItemBase item)
		{
			itemList.Remove(item);
			if (Application.isPlaying) Destroy(item);

			RefreshListIndex();
		}

		void RefreshListIndex()
		{

			for (int i = 0; i < itemList.Count; i++)
				itemList[i].index = i;
		}

		public VariableSourceObject AddConditionObject(Type type)
		{
			VariableSourceObject obj = CreateInstance(type) as VariableSourceObject;
			variableSource = obj;
			return obj;
		}

		public void RemoveConditionObject()
		{
			if (Application.isPlaying) Destroy(variableSource);
			variableSource = null;
		}
	}
}