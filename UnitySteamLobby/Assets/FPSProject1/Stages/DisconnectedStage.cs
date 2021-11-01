using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class DisconnectedStage : BaseStage
    {
        public override void OnEnter()
        {
            InitGameElements();
            InitStandardCanvas();

            // stage specific
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

            uiElements.Add(UI.UIElementType.DISCONNECTED);
            InstantiateStandardCanvas(UI.UIMenuType.UIMENU_DISCONNECTED, uiElements);

            _initializer.SERVER.EndServer();
            _initializer.CLIENT.EndClient();
            _initializer.STEAM_CONTROL.ShutDown();
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