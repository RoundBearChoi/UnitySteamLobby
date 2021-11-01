using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class MenuItem_Standard : MenuItem
    {
        public MenuItem_Standard(IGameInitializer baseInitializer, Color selectedColor, Color unSelectedColor, GameObject uiObject)
        {
            _initializer = baseInitializer;
            _currentDevice = baseInitializer.INPUT_DEVICES.GetInputDevice(0);

            _selectedColor = selectedColor;
            _unSelectedColor = unSelectedColor;

            _uiObject = uiObject;
            _text = uiObject.GetComponentInChildren<Text>();

            _initialSize = _text.rectTransform.localScale;
            _selectedSize = _initialSize * 1.1f;

            _onClickMenu = new OnClickMenu();
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnUpdate()
        {
            OnMouseHover();
            OnClick();
        }

        public override void OnLateUpdate()
        {
            OnSelection();
        }
    }
}