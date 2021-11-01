using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class ConnectingStage : BaseStage
    {
        float timer = 0f;

        public override void OnEnter()
        {
            InitGameElements();
            InitStandardCanvas();
        }

        public override void InitGameElements()
        {
            List<GameElements.GameElementType> gameElements = new List<GameElements.GameElementType>();

            InstantiateGameElements(gameElements);
        }

        public override void InitStandardCanvas()
        {
            List<UI.UIElementType> uiElements = new List<UI.UIElementType>();

            uiElements.Add(UI.UIElementType.CONNECTING);

            InstantiateStandardCanvas(UI.UIMenuType.UIMENU_CONNECTING, uiElements);
        }

        public override void OnFixedUpdate()
        {
            _standardCanvas.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            _standardCanvas.OnUpdate();

            timer += Time.deltaTime;

            if (timer >= 30f)
            {
                Message message = new Message_StageTransition(typeof(IntroStage));
                message.Register();
            }
        }

        public override void OnLateUpdate()
        {
            _standardCanvas.OnLateUpdate();
        }
    }
}