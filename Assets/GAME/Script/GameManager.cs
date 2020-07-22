using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	const string OptionFileName = "/Option.dat";

	public static OptionData optionData = new OptionData();
	public static int chapterCount = 0;

	EventSystem eventSystem;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadData();

		SetResolution(optionData.screenWidth, false);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		eventSystem = FindObjectOfType<EventSystem>();
	}

	private void LateUpdate()
	{
		if (eventSystem == null)
			return;

		if (eventSystem.currentSelectedGameObject != null)
			eventSystem.SetSelectedGameObject(null);
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
		{
			var height = Mathf.RoundToInt(width / (16f / 9f));
			Screen.SetResolution(width, height, false);
		}

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


