using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
	[Serializable]
	[CreateAssetMenu(fileName = "DialogGroup", menuName = "Dialog/Create new group", order = 1)]
	public class DialogGroupItem : ScriptableObject
	{
		public string id;
		public VariableSourceObject variableSource;

		public List<VariableObject> activeConditions = new List<VariableObject>();
		public List<DialogItem> dialogList = new List<DialogItem>();


		private void OnEnable()
		{
			if (string.IsNullOrEmpty(id))
				id = name;
		}

		public DialogItem AddDialogItem()
		{
			DialogItem dialog = CreateInstance(typeof(DialogItem)) as DialogItem;
			dialog.targetGroup = this;
			dialogList.Add(dialog);
			return dialog;
		}

		public void RemoveDialogItem(DialogItem item)
		{
			dialogList.Remove(item);
			if (Application.isPlaying) Destroy(item);
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