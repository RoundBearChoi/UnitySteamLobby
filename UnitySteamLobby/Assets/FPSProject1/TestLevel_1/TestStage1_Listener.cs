using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.GameElements;
using Steamworks;

namespace RB
{
    public class TestStage1_Listener : IListener
    {
        IGameInitializer _initializer = null;
        SteamSyncTestStage1 _stage = null;
        bool _remove = false;

        public bool REMOVE { get { return _remove; } set { _remove = value; } }

        public TestStage1_Listener(IGameInitializer initializer, SteamSyncTestStage1 stage)
        {
            _initializer = initializer;
            _stage = stage;
            MessageHandler_Network.listeners.Add(this);
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.SPAWN_POSITION_RECEIVED)
            {
                GameElement p = GameObject.Instantiate(_initializer.RESOURCE_LOADER.etcLoader.GetLoadedObj(etcResourceType.TEST_PLAYER)) as GameElement;
                Vector3 position = message.vec3s[0];
                p.transform.position = position;
                p.InitGameElement(_initializer);

                _stage.AddGameElement(p);
            }

            else if (message.MESSAGE_TYPE == MessageType.PLAYER_POSITION_RECEIVED)
            {
                SteamId steamID = message.steamIDs[0];

                if (_initializer.STEAM_CONTROL.IsLobbyMember(steamID))
                {
                    Vector3 position = message.vec3s[0];
                    _stage.OnPlayerPosition(steamID, position);
                }
            }
        }
    }
}