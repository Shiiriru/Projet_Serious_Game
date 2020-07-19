using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SoundPlayer
{
	static List<FmodSoundEvent> eventList = new List<FmodSoundEvent>();

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

	public static void PlayEvent(string id, string path, float volume = 1)
	{
		try
		{
			FmodSoundEvent evt = null;
			if (eventList.Where(e => e.id == id).FirstOrDefault() != null)
				evt = eventList.Where(e => e.id == id).FirstOrDefault();
			else
			{
				evt = new FmodSoundEvent(id);
				eventList.Add(evt);
			}

			evt.eventInstance = RuntimeManager.CreateInstance(path);
			evt.eventInstance.setVolume(volume);

			evt.eventInstance.start();
		}
		catch
		{
			Debug.LogError("Can not create a event for " + path);
		}
	}

	public static void StopEvent(string id, bool fadeOut)
	{
		if (eventList.Where(e => e.id == id).FirstOrDefault() == null)
			return;

		var evt = eventList.Where(e => e.id == id).FirstOrDefault();
		evt.eventInstance.stop(fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	public static void StopAllEvent()
	{
		foreach (var e in eventList)
			e.eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

		eventList.Clear();
	}
}

public class FmodSoundEvent
{
	public string id;
	public EventInstance eventInstance;
	public FmodSoundEvent(string _id)
	{
		id = _id;
	}
}