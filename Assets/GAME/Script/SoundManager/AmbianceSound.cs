using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
	List<FmodSoundEvent> eventList = new List<FmodSoundEvent>();

	public void Play(string id, string eventPath)
	{
		FmodSoundEvent evt = null;
		if (eventList.Where(e => e.id == id).FirstOrDefault() != null)
			evt = eventList.Where(e => e.id == id).FirstOrDefault();
		else
			evt = new FmodSoundEvent(id);

		evt.eventInstance.PlayEvent(eventPath);
	}

	public void Stop(string id, bool fadeOut)
	{
		if (eventList.Where(e => e.id == id).FirstOrDefault() == null)
			return;

		var evt = eventList.Where(e => e.id == id).FirstOrDefault();
		evt.eventInstance.stop(fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT : FMOD.Studio.STOP_MODE.IMMEDIATE);
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