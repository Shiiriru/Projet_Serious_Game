using UnityEngine;
using UnityEditor;
using DialogSystem;

public class LouisGoodVariableSource : VariableSource, ILouisGoodVariableSource
{
    public void SelectTextChoice(int index) { }
}

public interface ILouisGoodVariableSource
{
    void SelectTextChoice(int index);
}
