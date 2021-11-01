using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    [System.Serializable]
    public class UIMenu : MonoBehaviour
    {
        [Space(10)]
        [Header("Settings")]
        [SerializeField] protected Color _selectedColor = new Color();
        [SerializeField] protected Color _unSelectedColor = new Color();

        [Space(10)]
        [Header("Debug")]
        [SerializeField] protected IGameInitializer _initializer = null;
        
        [Space(10)]
        public Menu menu;

        public IGameInitializer INITIALIZER { get { return _initializer; } }

        public virtual void Init(IGameInitializer baseInitializer) { ClassDebug.Log("undefined"); }
        public virtual void OnFixedUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnLateUpdate() { ClassDebug.Log("undefined"); }

        public virtual void OnESC()
        {
            DetectedInputDevice inputDevice = _initializer.INPUT_DEVICES.GetInputDevice(0);

            if (inputDevice.KEYBOARD.escapeKey.wasPressedThisFrame)
            {
                UIDebug.Log("ESC pressed");
                _initializer.CURRENT_STAGE.OnESC();
            }
        }
    }
}