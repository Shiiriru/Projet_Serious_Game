using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem.Nodes
{
    [CreateNodeMenu("Image/Comic/Show")]
    [NodeWidth(250)]
    [NodeTint("#00ccff")]
    public class ShowComicNode : DurationNodeBase
    {
        // Use this for initialization
        protected override void Init()
        {
            name = "Show Comic";
            base.Init();
        }

		public GameObject comicPrefab;
        public int index;
    }
}
