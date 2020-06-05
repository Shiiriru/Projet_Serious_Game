﻿using UnityEngine;
using UnityEditor;

namespace DialogSystem.Nodes
{
	[CreateNodeMenu("Dialogue")]
	[NodeWidth(350)]
	[NodeTint("#99ffcc")]
	public class DialogNode : DialogNodeBase
	{
		// Use this for initialization
		protected override void Init()
		{
			name = "Dialogue";
			base.Init();
		}

		public string characterName;
		[TextArea] public string text;

		public bool disableScene = true;

		public bool displayAll;
		[Range(1, 5)] public int displaySpeed = 3;
	}
}