using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
	public class DialogItem : DialogItemBase
	{
		public string characterName;
		public string text;
		public List<BrancheItem> branches = new List<BrancheItem>();
	}
}