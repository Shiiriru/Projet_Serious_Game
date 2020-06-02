using UnityEngine;
using UnityEditor;
using DialogSystem;

public class OnboardingVariableSource : VariableSource, IOnboardingVariableSource
{

    public void SelectPhotoFace(int index) { }
}

public interface IOnboardingVariableSource
{
    void SelectPhotoFace(int index);
}