using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RB
{
    [System.Serializable]
    public class InputDevices
    {
        [SerializeField]
        IGameInitializer _initializer = null;

        [SerializeField]
        List<DetectedInputDevice> _listInputDevices;

        public void Init(IGameInitializer initializer)
        {
            _initializer = initializer;
            _listInputDevices = new List<DetectedInputDevice>();

            AddInputDevice(Keyboard.current, Mouse.current, null);
        }

        public void AddInputDevice(Keyboard keyboard, Mouse mouse, Gamepad gamepad)
        {
            DetectedInputDevice device = new DetectedInputDevice(keyboard, mouse, gamepad);
            _listInputDevices.Add(device);
        }

        public DetectedInputDevice GetInputDevice(int index)
        {
            if (_listInputDevices.Count > index)
            {
                return _listInputDevices[index];
            }
            else
            {
                return new DetectedInputDevice(null, null, null);
            }
        }
    }
}