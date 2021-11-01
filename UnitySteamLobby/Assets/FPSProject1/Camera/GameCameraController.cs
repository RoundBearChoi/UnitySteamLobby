using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    [System.Serializable]
    public class GameCameraController
    {
        IGameInitializer _initializer = null;
        [SerializeField] CameraControl_Listener _cameraStageTransitionListener = null;
        [SerializeField] GameCamera _gameCamera = null;

        public GameCamera GAME_CAMERA { get { return _gameCamera; } }

        public GameCameraController(IGameInitializer initializer)
        {
            _initializer = initializer;
            _cameraStageTransitionListener = new CameraControl_Listener(initializer, this);
        }

        public void SetCurrentCamera(GameCamera gameCamera)
        {
            _gameCamera = gameCamera;
        }
    }
}