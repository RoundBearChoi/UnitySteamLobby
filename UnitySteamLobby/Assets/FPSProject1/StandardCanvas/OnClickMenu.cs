using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    [System.Serializable]
    public class OnClickMenu
    {
        [SerializeField] protected string _actionName = "undefined action";

        public virtual void Do() { ClassDebug.Log("undefined"); }
    }
}