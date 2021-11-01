using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class IntroStage : BaseStage
    {
        public override void OnEnter()
        {
            InitGameElements();
            InitStandardCanvas();

            // init steam
            if (_initializer.STEAM_CONTROL.InitSteamClient(_initializer))
            {

            }
            else
            {
                SteamDebug.Log("failed instantiating steam manager.. transitioning to failed-to-sync-with-steam stage..");

                Message message = new Message_StageTransition(typeof(FailedToSyncWithSteamStage));
                message.Register();
            }

            _initializer.SERVER.EndServer();
            _initializer.CLIENT.EndClient();
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

            InstantiateStandardCanvas(UI.UIMenuType.UIMENU_INTRO, uiElements);
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