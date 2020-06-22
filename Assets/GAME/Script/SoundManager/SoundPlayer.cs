using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public static class SoundPlayer
{
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
			eventInstance.start();
		}
		catch
		{
			Debug.LogError("Can not create a event for " + path);
		}
	}
}
