using UnityEngine;
using UnityEditor;
using DialogSystem;

public class SoldatVariableSource : VariableSource, ISoldatVariableSource
{
	public bool activeChoice1;
	public int testInt;

	public void MakeChoice(int index) { }
	public void TestFunc(){ }
}

public interface ISoldatVariableSource
{
	void MakeChoice(int index);
	void TestFunc();
}