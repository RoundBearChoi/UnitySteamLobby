using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public interface IGameInitializer
    {
        public Transform TRANSFORM { get; }
        public BaseStage CURRENT_STAGE { get; }
        public ResourceLoader RESOURCE_LOADER { get; }
        public InputDevices INPUT_DEVICES { get; }
        public SteamIntegration.ISteamControl STEAM_CONTROL { get; }
        public Server.IServer SERVER { get; }
        public Client.Client CLIENT { get; }
        public GameCameraController GAMECAMC_CONTROLLER { get; }

        public void AddNextStage(System.Type stageType);
        public void MakeStageTransition();
        public void SetEmptyStage();
        public void InitClient();
        public Coroutine RunRoutine(IEnumerator enumerator);
    }
}