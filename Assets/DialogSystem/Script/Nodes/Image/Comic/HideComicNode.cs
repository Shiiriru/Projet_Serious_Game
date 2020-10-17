using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Nodes
{
    [CreateNodeMenu("Image/Comic/Hide")]
    [NodeWidth(250)]
    [NodeTint("#00ccff")]
    public class HideComicNode : DurationNodeBase
    {
        // Use this for initialization
        protected override void Init()
        {
            name = "Hide Comic";
            base.Init();
        }

        public bool hideAll;
        public GameObject comicPrefab;
        public int index;
    }
}