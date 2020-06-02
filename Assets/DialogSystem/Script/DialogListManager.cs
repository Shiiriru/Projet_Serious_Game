using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogSystem
{
	public class DialogListManager : MonoBehaviour
	{
		public List<DialogGroupItem> dialogList = new List<DialogGroupItem>();

		public DialogGroupItem AddGroup()
		{
			var newGroup = ScriptableObject.CreateInstance(typeof(DialogGroupItem)) as DialogGroupItem;
			dialogList.Add(newGroup);
			return newGroup;
		}

		//public void RemoveGroup(DialogGroupItem group)
		//{
		//	dialogList.Remove(group);
		//	if (Application.isPlaying) Destroy(group);
		//}

		public DialogGroupItem FindDialogById(string id)
		{
			var group = dialogList.Where(d => d.id == id).FirstOrDefault();
			return group;
		}

		public DialogGroupItem[] FindDialogsById(string id)
		{
			return dialogList.Where(d => d.id == id).ToArray();
		}
	}
}