using DialogSystem;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class VariableObjectMsgGood : VariableSourceObject
{
   MsgGoodVariableSource source = new MsgGoodVariableSource();

    public override FieldInfo[] GetSourceFieldInfos()
    {
        return source.GetFieldInfos();
    }

    public override string[] GetSourceMethods()
    {
        return source.GetMethods();
    }
}
