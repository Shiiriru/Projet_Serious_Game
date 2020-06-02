using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class VariableObjectOnboarding : VariableSourceObject
{
    OnboardingVariableSource source = new OnboardingVariableSource();

    //can only support int, float, bool, string
    public override FieldInfo[] GetSourceFieldInfos()
    {
        return source.GetFieldInfos();
    }

    public override string[] GetSourceMethods()
    {
        return source.GetMethods();
    }
}
