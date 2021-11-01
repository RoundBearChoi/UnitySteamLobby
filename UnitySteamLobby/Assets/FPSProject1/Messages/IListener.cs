using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public interface IListener
    {
        public bool REMOVE { get; set; }

        public void OnNotify(Message message);
    }
}