using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem
{
	public class DialogItemBase : ScriptableObject
	{
		[HideInInspector] public int index;
		[HideInInspector] public DialogGroupItem targetGroup;
	}
}
