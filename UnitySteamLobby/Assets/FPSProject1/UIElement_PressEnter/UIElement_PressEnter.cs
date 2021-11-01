using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class UIElement_PressEnter : UIElementObj
    {
        [SerializeField]
        Text _text = null;

        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;

            _text = this.GetComponentInChildren<Text>();
        }

        public override void OnUpdate()
        {
            if (_initializer.INPUT_DEVICES.GetInputDevice(0).KEYBOARD.enterKey.wasPressedThisFrame)
            {
                GeneralDebug.Log("enter pressed on title stage.. transitioning to intro stage");

                Message message = new Message_StageTransition(typeof(IntroStage));
                message.Register();
            }
        }
    }
}