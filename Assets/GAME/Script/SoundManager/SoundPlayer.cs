using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public static class SoundPlayer
{
	static List<EventInstance> events = new List<EventInstance>();

	public static void PlayOneShot(string path)
	{
		try
		{
			RuntimeManager.PlayOneShot(path);
		}
		catch
		{
			Debug.LogError("Can not play " + path);
		}
	}

	public static void PlayEvent(ref this EventInstance eventInstance, string path)
	{
		try
		{
			eventInstance = RuntimeManager.CreateInstance(path);
			events.Add(eventInstance);
			eventInstance.start();
		}
		catch
		{
			Debug.LogError("Can not create a event for " + path);
		}
	}

	public static void StopAllEvent()
	{
		foreach (var e in events)
			e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

		events.Clear();
	}
}
