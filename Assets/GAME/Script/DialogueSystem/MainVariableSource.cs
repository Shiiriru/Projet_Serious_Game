using UnityEngine;
using UnityEditor;
using DialogSystem;

public class MainVariableSource : VariableSource, IMainVariableSource
{
	public bool LetterChecked;
    public bool MapChecked;
    public bool PhotoChecked;
    public bool GameSpotGermanFinished;
    public bool MaskChecked;
    public bool CardGameWon;

	public void OnBordingSelectPhotoFace(int index){ }
    public void LaunchGameSpotGerman() { }
    public void LaunchCardGame() { }
    public void LaunchEndChapter() { }
    public void DropLootMask() { }
    public void DropLootMontre() { }
}

public interface IMainVariableSource
{
	void OnBordingSelectPhotoFace(int index);
    void LaunchGameSpotGerman();
    void LaunchCardGame();
    void LaunchEndChapter();
    void DropLootMask();
    void DropLootMontre();
}
