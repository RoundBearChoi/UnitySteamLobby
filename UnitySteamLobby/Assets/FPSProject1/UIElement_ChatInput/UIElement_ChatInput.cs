using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class UIElement_ChatInput : UIElementObj
    {
        [SerializeField] InputField _inputField = null;
        [SerializeField] Text _text = null;
        [SerializeField] bool _activateField = false;
        [SerializeField] bool _dontMoveCursor = false;

        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;
            
            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach(Transform t in arr)
            {
                if (t.name.Equals("InputField"))
                {
                    _inputField = t.GetComponent<InputField>();
                }
                else if (t.name.Equals("TextField"))
                {
                    _text = t.GetComponent<Text>();
                }
            }
        }

        public override void OnUpdate()
        {
            if (_initializer.INPUT_DEVICES.GetInputDevice(0).KEYBOARD.enterKey.wasPressedThisFrame)
            {
                if (!_activateField)
                {
                    _activateField = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_inputField.text))
                    {
                        SendChat();
                    }
                    else
                    {
                        _activateField = false;
                    }
                }
            }
            else if (_initializer.INPUT_DEVICES.GetInputDevice(0).MOUSE.leftButton.wasPressedThisFrame)
            {
                if (MouseHover.IsHovering(_text.rectTransform, _initializer.INPUT_DEVICES.GetInputDevice(0).MOUSE))
                {
                    _activateField = true;
                    _dontMoveCursor = true;
                }
                else
                {
                    _activateField = false;
                }
            }
        }

        public override void OnLateUpdate()
        {
            if (_activateField)
            {
                _inputField.ActivateInputField();
                _inputField.interactable = true;

                if (!string.IsNullOrEmpty(_inputField.text))
                {
                    if (!_dontMoveCursor)
                    {
                        _inputField.MoveTextEnd(true);
                    }
                }
            }
            else
            {
                _inputField.DeactivateInputField();
                _inputField.interactable = false;
                _dontMoveCursor = false;
            }
        }

        void SendChat()
        {
            UIDebug.Log("input field: " + _inputField.text);

            Network.HandShakenPlayer[] arr = null;

            if (_initializer.SERVER.SERVER_STARTED)
            {
                arr = _initializer.SERVER.GetAllHandShakenPlayers();
            }
            else
            {
                arr = _initializer.CLIENT.GetAllHandShakenPlayers();
            }

            Message m = new Message_ChatSent();
            m.strings = new List<string>();
            m.strings.Add(_inputField.text);
            m.Register();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].mSteamID.Value != _initializer.STEAM_CONTROL.STEAM_ID.Value)
                {
                    SteamDebug.Log("sending chat to: " + arr[i].mSteamName);
                    _initializer.STEAM_CONTROL.Send_Chat(arr[i].mSteamID, _inputField.text);
                }
            }

            _inputField.text = string.Empty;
            _activateField = true;
        }
    }
}