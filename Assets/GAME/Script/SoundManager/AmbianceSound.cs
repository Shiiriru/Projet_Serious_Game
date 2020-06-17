using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
	EventInstance eventInstance;

	public void Play(string eventPath)
	{
		eventInstance.PlayEvent(eventPath);
	}

	public void Stop(bool fadeOut)
	{
		eventInstance.stop(fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
}
