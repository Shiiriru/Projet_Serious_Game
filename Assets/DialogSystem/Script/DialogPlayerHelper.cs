using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogSystem.Nodes;
using XNode;
using System.Linq;
using System;

namespace DialogSystem
{
	[RequireComponent(typeof(DialogPlayer))]
	public class DialogPlayerHelper : MonoBehaviour
	{
		static DialogPlayer dialogPlayer;
		public static VariableSourceManager VariableSourceMgr { get; private set; }

		private void Awake()
		{
			dialogPlayer = GetComponent<DialogPlayer>();
		}

		public static void SetDialog(DialogGraph target, bool autoPlay = true)
		{
			dialogPlayer.SetDialog(target, autoPlay);
		}

		//public static void Play()
		//{
		//	dialogPlayer.PlayDialog();
		//}

		public static void SetVariableSourceMgr(VariableSourceManager mgr)
		{
			VariableSourceMgr = mgr;
		}
		public static void SetOnFinishedAction(Action action)
		{
			dialogPlayer.onDialogFinished += action;
		}

		public static void RemoveOnFinishedAction(Action action)
		{
			dialogPlayer.onDialogFinished -= action;
		}

		public static bool IsPlaying()
		{
			return dialogPlayer.isPLaying;
		}
	}
}