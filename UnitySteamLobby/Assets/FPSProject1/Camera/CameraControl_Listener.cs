using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class CameraControl_Listener : IListener
    {
        IGameInitializer _initializer = null;
        GameCameraController _gameCameraController = null;

        public bool REMOVE { get { return false; } set { } }

        public CameraControl_Listener(IGameInitializer initializer, GameCameraController gameCameraController)
        {
            _initializer = initializer;
            _gameCameraController = gameCameraController;

            MessageHandler_StageTransition.listeners.Add(this);
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.STAGE_TRANSITION)
            {
                System.Type stageType = message.types[0];

                if (stageType == typeof(TitleStage))
                {
                    InstantiateCamera(etcResourceType.DEFAULT_CAMERA);
                }

                else if (stageType == typeof(IntroStage))
                {
                    InstantiateCamera(etcResourceType.DEFAULT_CAMERA);
                }

                else if (stageType == typeof(SteamSyncTestStage1))
                {
                    InstantiateCamera(etcResourceType.TEST_LEVEL_1_CAMERA);
                }
            }
        }

        public void InstantiateCamera(etcResourceType resourceType)
        {
            if (_gameCameraController.GAME_CAMERA != null)
            {
                GameObject.Destroy(_gameCameraController.GAME_CAMERA.gameObject);
            }

            GameCamera newCam = GameObject.Instantiate(_initializer.RESOURCE_LOADER.etcLoader.GetLoadedObj(resourceType)) as GameCamera;
            newCam.InitCam(_initializer);
            newCam.transform.SetParent(_initializer.TRANSFORM);

            _gameCameraController.SetCurrentCamera(newCam);
        }
    }
}