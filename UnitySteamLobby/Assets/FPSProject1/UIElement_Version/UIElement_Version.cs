using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class UIElement_Version : UIElementObj
    {
        [SerializeField]
        Text _text = null;

        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;

            _text = this.GetComponentInChildren<Text>();
            _text.text = Application.version;

            GeneralDebug.Log("game version: " + _text.text);
        }
    }
}