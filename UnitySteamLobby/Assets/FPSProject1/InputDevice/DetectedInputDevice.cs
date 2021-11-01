using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RB
{
    [System.Serializable]
    public class DetectedInputDevice
    {
        Keyboard _keyboard = null;
        Mouse _mouse = null;
        Gamepad _gamepad = null;

        [SerializeField] string _keyboardName;
        [SerializeField] string _mouseName;
        [SerializeField] string _gamepadName;

        public Keyboard KEYBOARD { get { return _keyboard; } }
        public Mouse MOUSE { get { return _mouse; } }

        public DetectedInputDevice(Keyboard keyboard, Mouse mouse, Gamepad gamepad)
        {
            _keyboard = keyboard;
            _mouse = mouse;
            _gamepad = gamepad;

            SetNames();
        }

        void SetNames()
        {
            _keyboardName = string.Empty;
            _mouseName = string.Empty;
            _gamepadName = string.Empty;

            if (_keyboard != null)
            {
                _keyboardName = _keyboard.name;
            }
            else
            {
                _keyboard = new Keyboard();
            }

            if (_mouse != null)
            {
                _mouseName = _mouse.name;
            }
            else
            {
                _mouse = new Mouse();
            }

            if (_gamepad != null)
            {
                _gamepadName = _gamepad.name;
            }
            else
            {
                _gamepad = new Gamepad();
            }
        }
    }
}