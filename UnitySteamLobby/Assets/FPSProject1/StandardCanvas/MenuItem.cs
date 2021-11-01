using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    [System.Serializable]
    public class MenuItem
    {
        [Header("Debug")]
        [Space(5)] [SerializeField] protected IGameInitializer _initializer = null;
        [Space(5)] [SerializeField] protected DetectedInputDevice _currentDevice = null;
        [Space(5)] [SerializeField] protected Color _selectedColor;
        [Space(5)] [SerializeField] protected Color _unSelectedColor;
        [Space(5)] [SerializeField] protected GameObject _uiObject = null;
        [Space(5)] [SerializeField] protected Text _text = null;
        [Space(5)] [SerializeField] protected Vector3 _initialSize = new Vector3();
        [Space(5)] [SerializeField] protected Vector3 _selectedSize = new Vector3();
        [Space(5)] [SerializeField] protected bool _isHighlighted = false;
        [Space(5)] public OnClickMenu _onClickMenu;

        public virtual void OnFixedUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnLateUpdate() { ClassDebug.Log("undefined"); }

        public virtual void OnMouseHover()
        {
            if (MouseHover.IsHovering(_text.rectTransform, _currentDevice.MOUSE))
            {
                _isHighlighted = true;
            }
            else
            {
                _isHighlighted = false;
            }
        }

        public virtual void OnSelection()
        {
            if (_isHighlighted)
            {
                _text.color = _selectedColor;
                _text.rectTransform.localScale = Vector3.Lerp(_text.rectTransform.localScale, _selectedSize, Time.deltaTime * 15f);
            }
            else
            {
                _text.color = _unSelectedColor;
                _text.rectTransform.localScale = Vector3.Lerp(_text.rectTransform.localScale, _initialSize, Time.deltaTime * 15f);
            }
        }

        public virtual void OnClick()
        {
            if (_isHighlighted)
            {
                if (_currentDevice.MOUSE.leftButton.wasPressedThisFrame)
                {
                    _onClickMenu.Do();
                }
            }
        }
    }
}