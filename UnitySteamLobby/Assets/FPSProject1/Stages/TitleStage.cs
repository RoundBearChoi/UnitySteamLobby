using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class TitleStage : BaseStage
    {
        public override void OnEnter()
        {
            InitGameElements();
            InitStandardCanvas();

            _initializer.SERVER.OpenPort(_initializer);
            _initializer.STEAM_CONTROL.InitSteamClient(_initializer);
        }

        public override void InitGameElements()
        {
            List<GameElements.GameElementType> gameElements = new List<GameElements.GameElementType>();

            InstantiateGameElements(gameElements);
        }

        public override void InitStandardCanvas()
        {
            List<UI.UIElementType> uiElements = new List<UI.UIElementType>();

            uiElements.Add(UI.UIElementType.VERSION);
            uiElements.Add(UI.UIElementType.PRESS_ENTER);

            InstantiateStandardCanvas(UI.UIMenuType.NONE, uiElements);
        }

        public override void OnFixedUpdate()
        {
            _standardCanvas.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            _standardCanvas.OnUpdate();
        }

        public override void OnLateUpdate()
        {
            _standardCanvas.OnLateUpdate();
        }
    }
}