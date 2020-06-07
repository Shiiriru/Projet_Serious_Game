using UnityEngine;
using UnityEditor;
using DialogSystem;

public class MainVariableSource : VariableSource, IMainVariableSource
{
	//chapter 1
	public bool LetterChecked;
    public bool MapChecked;
    public bool villagePhotoChecked;
	public bool MaskChecked;
	public bool CardGameWon;
	public void LaunchCardGame() { }
	public void LootWatch() { }
	public void LootMask() { }

	//chapter 2
	public bool GameSpotGermanFinished;

	public void OnBordingSelectPhotoFace(int index){ }
    public void LaunchGameSpotGerman() { }
    public void LaunchEndChapter() { }
}

public interface IMainVariableSource
{
	void OnBordingSelectPhotoFace(int index);
    void LaunchGameSpotGerman();
    void LaunchCardGame();
    void LaunchEndChapter();
    void LootMask();
    void LootWatch();
}
