using FMOD.Studio;
using FMODUnity;

public static class SoundPlayer
{
	public static void PlayOneShot(string path)
	{
		RuntimeManager.PlayOneShot(path);
	}

	public static void PlayEvent(ref this EventInstance eventInstance, string path)
	{
		eventInstance = RuntimeManager.CreateInstance(path);
		eventInstance.start();
	}
}
