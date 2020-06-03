using UnityEngine;
using UnityEditor;
using DialogSystem;

public class MsgGoodVariableSource : VariableSource, IMsgGoodVariableSource
{
    public void ValidationEnvoiMsg(int index) { }
}

public interface IMsgGoodVariableSource
{
    void ValidationEnvoiMsg(int index);
}
