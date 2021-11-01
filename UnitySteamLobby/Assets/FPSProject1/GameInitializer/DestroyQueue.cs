using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class DestroyQueue : MonoBehaviour
    {
        IGameInitializer _initializer = null;

        private void Start()
        {
            _initializer = FindObjectOfType<GameInitializer>();   
        }

        private void OnDestroy()
        {
            ClassDebug.Log("on destroy..");

            _initializer.SERVER.EndServer();
            _initializer.CLIENT.EndClient();
            _initializer.STEAM_CONTROL.ShutDown();
        }

        private void OnApplicationQuit()
        {
            _initializer.SERVER.EndServer();
            _initializer.CLIENT.EndClient();
            _initializer.STEAM_CONTROL.ShutDown();
        }
    }
}