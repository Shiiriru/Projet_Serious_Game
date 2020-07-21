using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;

public class GameManager : MonoBehaviour
{
	const string OptionFileName = "/Option.dat";

	public static OptionData optionData = new OptionData();

	public static int chapterCount = 0;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadData();

		SetResolution(optionData.screenWidth, false);
	}

	public static void SetResolution(int width, bool save = true)
	{
		//same width
		if (Screen.width == width)
			return;
		optionData.screenWidth = width;
		//16 :9
		if (width == 0)
			Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
		else
			Screen.SetResolution(width, width / (16 / 9), false);

		if (save)
			SaveData();
	}

	public static void SetVolume(float volume)
	{

	}

	public static void SaveData()
	{
		FileStream file = null;

		try
		{
			BinaryFormatter bf = new BinaryFormatter();
			file = File.Create(Application.persistentDataPath + OptionFileName);
			bf.Serialize(file, optionData);

		}
		catch (Exception e) { Debug.LogError(e); }
		finally
		{
			if (file != null)
				file.Close();
		}
	}

	public static void LoadData()
	{
		FileStream file = null;
		try
		{
			BinaryFormatter bf = new BinaryFormatter();
			file = File.Open(Application.persistentDataPath + OptionFileName, FileMode.Open);
			optionData = bf.Deserialize(file) as OptionData;
		}
		//create new data
		catch { SaveData(); }
		finally
		{
			if (file != null)
				file.Close();
		}
	}
}

[Serializable]
public class OptionData
{
	public int screenWidth = 1600;
	public float volume = 1;
}


