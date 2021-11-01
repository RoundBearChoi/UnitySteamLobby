using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using RB.Network;

namespace RB.GameElements
{
    public class TestLevel1_Ground : GameElement
    {
        [SerializeField] List<SpawnPoint> _listSpawnPoints;

        public override void InitGameElement(IGameInitializer initializer)
        {
            _initializer = initializer;
            _listSpawnPoints = new List<SpawnPoint>();

            ShuffleSpawnPoints();
            SendSpawnPoints();
        }

        void ShuffleSpawnPoints()
        {
            SpawnPoint[] arr = this.gameObject.GetComponentsInChildren<SpawnPoint>();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].randomInt = Random.Range(0, 1000);
            }

            for (int i = 0; i < 1000; i++)
            {
                for (int x = 0; x < arr.Length; x++)
                {
                    if (arr[x].randomInt == i)
                    {
                        _listSpawnPoints.Add(arr[x]);
                    }
                }
            }
        }

        void SendSpawnPoints()
        {
            if (_initializer.SERVER.SERVER_STARTED)
            {
                HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();

                for (int i = 0; i < arr.Length; i++)
                {
                    if (_listSpawnPoints.Count > i)
                    {
                        GeneralDebug.Log("random spawn " + _listSpawnPoints[i].name + ".. " + _listSpawnPoints[i].transform.position + ".. assigned to " + arr[i].mSteamName + "..");
                        _initializer.STEAM_CONTROL.Send_SpawnPoint(arr[i].mSteamID, _listSpawnPoints[i].transform.position);
                    }
                }
            }
        }
    }
}