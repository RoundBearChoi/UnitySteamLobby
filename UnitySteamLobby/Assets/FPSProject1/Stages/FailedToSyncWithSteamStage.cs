using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class FailedToSyncWithSteamStage : BaseStage
    {
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

            uiElements.Add(UI.UIElementType.FAILED_TO_SYNC_WITH_STEAM);

            InstantiateStandardCanvas(UI.UIMenuType.UIMENU_FAILED_TO_SYNC_WITH_STEAM, uiElements);
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