using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Nodes
{
    public class HideComicNode : DurationNodeBase
    {
        public bool hideAll;
        public ComicController comic;
        public int index;
    }
}