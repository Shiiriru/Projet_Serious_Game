using UnityEngine;
using UnityEditor;
using DialogSystem;

public class MainVariableSource : VariableSource, IMainVariableSource
{
	public bool isPhotoSmile;

	public bool isTalkedToLouis;
	public bool isCommandantCalled;

	public void SendLetter(int chapter) { }
	public void LoseInventoryObject(string id) { }


	public void OnBordingSelectPhotoFace(bool smile) { }

	//chapter 1
	public bool LetterChecked;
	public bool letterSent;

	public bool MapChecked;
	public bool villagePhotoChecked;
	public bool louisLeave;

	public bool MaskChecked;
	public bool CardGameWon;
	public void LaunchCardGame() { }
	public void LootWatch() { }
	public void LootMask() { }

	//chapter 2
	public bool isUsedNewMask;
	public bool spotGermanFinished;
	public void LaunchGameSpotGerman() { }
	public void AfterGasAttack() { }
	public void WearMask(bool newMask) { }

	//chapter 3
	public bool badgeChecked;
	public bool badgeGived;
	public void LaunchFinalWar() { }
}

public interface IMainVariableSource
{
	void OnBordingSelectPhotoFace(bool smile);
	void LaunchGameSpotGerman();
	void LaunchCardGame();
	void LootMask();
	void LootWatch();
	void SendLetter(int chapter);
	void WearMask(bool newMask);
	void AfterGasAttack();
	void LaunchFinalWar();

	void LoseInventoryObject(string id);
}
