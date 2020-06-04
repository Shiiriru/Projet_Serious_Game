using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogSystem
{
	public class DialogListManager : MonoBehaviour
	{
		public List<DialogGraph> dialogList = new List<DialogGraph>();

		//public DialogGroupItem FindDialogById(string id)
		//{
		//	var group = dialogList.Where(d => d.id == id).FirstOrDefault();
		//	return group;
		//}

		//public DialogGroupItem[] FindDialogsById(string id)
		//{
		//	return dialogList.Where(d => d.id == id).ToArray();
		//}
	}
}