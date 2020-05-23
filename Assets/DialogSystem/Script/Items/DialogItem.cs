﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
	[Serializable]
	public class DialogItem : ScriptableObject
	{
		public string characterName;
		public string text;
		public List<BrancheItem> branches = new List<BrancheItem>();

		[HideInInspector] public DialogGroupItem targetGroup;
	}
}