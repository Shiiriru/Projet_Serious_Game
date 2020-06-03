using UnityEngine;
using UnityEditor;
using DialogSystem;

public class MainVariableSource : VariableSource, IMainVariableSource
{
	public bool LetterChecked;

	public void OnBordingSelectPhotoFace(int index){ }
}

public interface IMainVariableSource
{
	void OnBordingSelectPhotoFace(int index);
}
