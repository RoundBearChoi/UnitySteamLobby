using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Network;

namespace RB.UI
{
    public class UIElement_PlayersList : UIElementObj
    {
        [Header("Debug")]
        [SerializeField] List<PlayerSlot> _listPlayerSlots;

        public override void Init(IGameInitializer initializer)
        {
            _initializer = initializer;
            _listener = new PlayersList_Listener(initializer, this);

            _listPlayerSlots = new List<PlayerSlot>();

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.name.Contains("PlayerSlot"))
                {
                    _listPlayerSlots.Add(new PlayerSlot(t.gameObject, _listPlayerSlots.Count + 1));
                }
            }

            UpdatePlayersList();
        }

        public override void OnFixedUpdate()
        {

        }

        public void UpdatePlayersList()
        {
            if (_initializer.SERVER.SERVER_STARTED)
            {
                HandShakenPlayer[] arr = _initializer.SERVER.GetAllHandShakenPlayers();
                UpdateUIList(arr);
            }
            else
            {
                HandShakenPlayer[] arr = _initializer.CLIENT.GetAllHandShakenPlayers();
                UpdateUIList(arr);
            }
        }

        void UpdateUIList(HandShakenPlayer[] handShakenPlayers)
        {
            for (int i = 0; i < _listPlayerSlots.Count; i++)
            {
                if (i < handShakenPlayers.Length)
                {
                    _listPlayerSlots[i].Update(handShakenPlayers[i]);
                }
                else
                {
                    _listPlayerSlots[i].Reset();
                }
            }
        }
    }
}