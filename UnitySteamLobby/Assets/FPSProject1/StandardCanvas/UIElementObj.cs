using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    public class UIElementObj : MonoBehaviour
    {
        protected IGameInitializer _initializer = null;
        protected IListener _listener = null;

        public virtual void Init(IGameInitializer initializer)
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        private void OnDestroy()
        {
            if (_listener != null)
            {
                _listener.REMOVE = true;
            }
        }
    }
}