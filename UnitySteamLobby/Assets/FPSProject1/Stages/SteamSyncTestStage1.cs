using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.GameElements;
using Steamworks;

namespace RB
{
    public class SteamSyncTestStage1 : BaseStage
    {
        [SerializeField]
        Dictionary<ulong, GameElement> _dicPositions;

        public override void OnEnter()
        {
            InitGameElements();
            InitStandardCanvas();

            _dicPositions = new Dictionary<ulong, GameElement>();
            _listener = new TestStage1_Listener(_initializer, this);
        }

        public override void InitGameElements()
        {
            List<GameElementType> gameElements = new List<GameElementType>();

            gameElements.Add(GameElementType.TEST_LEVEL_1_GROUND);
            gameElements.Add(GameElementType.TEST_LEVEL_1_LIGHTING);

            InstantiateGameElements(gameElements);
        }

        public override void InitStandardCanvas()
        {
            List<UI.UIElementType> uiElements = new List<UI.UIElementType>();

            InstantiateStandardCanvas(UI.UIMenuType.NONE, uiElements);
        }

        public override void OnFixedUpdate()
        {
            foreach (GameElement e in _listGameElements)
            {
                e.OnFixedUpdate();
            }

            _standardCanvas.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            foreach(GameElement e in _listGameElements)
            {
                e.OnUpdate();
            }

            _standardCanvas.OnUpdate();
        }

        public override void OnLateUpdate()
        {
            foreach (GameElement e in _listGameElements)
            {
                e.OnLateUpdate();
            }

            _standardCanvas.OnLateUpdate();
        }

        public void OnPlayerPosition(SteamId steamID, Vector3 position)
        {
            if (!_dicPositions.ContainsKey(steamID.Value))
            {
                GameElement p = Instantiate(_initializer.RESOURCE_LOADER.etcLoader.GetLoadedObj(etcResourceType.DUMMY_PLAYER)) as GameElement;
                p.transform.position = position;
                p.InitGameElement(_initializer);

                string name = _initializer.STEAM_CONTROL.GetMemberName(steamID);
                GeneralDebug.Log(name + " dummy spawn position: " + position);

                AddGameElement(p);
                _dicPositions.Add(steamID, p);
            }
            else
            {
                _dicPositions[steamID].SetTargetPosition(position);
            }
        }
    }
}