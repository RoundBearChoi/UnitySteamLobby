using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class GameInitializer : MonoBehaviour, IGameInitializer
    {
        [Header("Debug")]
        [Space(10)] [SerializeField] ResourceLoader _resourceLoader;
        [Space(10)] [SerializeField] InputDevices _inputDevices;
        [Space(10)] [SerializeField] StartingStageType _startingStageType;
        [Space(10)] [SerializeField] SteamIntegration.SteamControl _steamControl;
        [Space(10)] [SerializeField] Server.Server _server;
        [Space(10)] [SerializeField] Client.Client _client;
        [Space(10)] [SerializeField] List<MessageHandler> _listMessageHandlers;
        [Space(10)] [SerializeField] GameCameraController _gameCameraController;
        [Space(10)] [SerializeField] Sound.MenuSoundController _menuSoundController;
        [Space(10)] [SerializeField] GameData _gameData;
        [Space(10)] [SerializeField] BaseStage _currentStage = null;
        [Space(10)] [SerializeField] bool _showDebugText;
        
        protected List<BaseStage> _listNextStage = new List<BaseStage>();

        public ResourceLoader RESOURCE_LOADER { get { return _resourceLoader; } }
        public InputDevices INPUT_DEVICES { get { return _inputDevices; } }
        public SteamIntegration.ISteamControl STEAM_CONTROL { get { return _steamControl; } }
        public Server.IServer SERVER { get { return _server; } }
        public Client.Client CLIENT { get { return _client; } }
        public GameCameraController GAMECAMC_CONTROLLER { get { return _gameCameraController; } }
        public BaseStage CURRENT_STAGE { get { return _currentStage; } }
        public Transform TRANSFORM { get { return this.transform; } }

        private void Start()
        {
            BaseDebugger.InitLogFile();

            _resourceLoader.Init(this);
            _inputDevices.Init(this);
            
            _listMessageHandlers.Add(new MessageHandler_StageTransition(this));
            _listMessageHandlers.Add(new MessageHandler_Network(this));

            _gameCameraController = new GameCameraController(this); //setup camera controller & listener after message handlers are setup
            _menuSoundController = new Sound.MenuSoundController(this);
            _gameData = new GameData();

            SetEmptyStage();

            if (_startingStageType == StartingStageType.TITLE_STAGE)
            {
                Message m = new Message_StageTransition(typeof(TitleStage));
                m.Register();
            }
            else if(_startingStageType == StartingStageType.INTRO_STAGE)
            {
                Message m = new Message_StageTransition(typeof(IntroStage));
                m.Register();
            }

            GameObject destroyQueue = new GameObject("OnDestroy");
            destroyQueue.AddComponent<DestroyQueue>();
            destroyQueue.transform.SetParent(this.transform, true);
        }

        private void Update()
        {
            _steamControl.OnUpdate();
            _currentStage.OnUpdate();
            _menuSoundController.OnUpdate();

            BaseDebugger.ShowDebugText(_showDebugText);
        }

        private void FixedUpdate()
        {
            _steamControl.OnFixedUpdate();
            _currentStage.OnFixedUpdate();

            MakeStageTransition();
        }

        private void LateUpdate()
        {
            _steamControl.OnLateUpdate();
            _currentStage.OnLateUpdate();

            foreach(MessageHandler handler in _listMessageHandlers)
            {
                handler.HandleMessages();
            }
        }

        public void AddNextStage(System.Type stageType)
        {
            if (stageType.IsSubclassOf(typeof(BaseStage)))
            {
                GameObject obj = new GameObject();
                obj.transform.SetParent(this.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.name = stageType.Name;

                BaseStage newStage = obj.AddComponent(stageType) as BaseStage;
                newStage.InitStage(this);

                GeneralDebug.Log("instantiated stage gameobject: " + obj.name);

                _listNextStage.Add(newStage);
            }
        }

        public void MakeStageTransition()
        {
            if (_listNextStage.Count > 0)
            {
                if (_currentStage != null)
                {
                    _currentStage.OnExit();
                    Destroy(_currentStage.gameObject);
                }

                _listNextStage[0].OnEnter();
                _currentStage = _listNextStage[0];

                _listNextStage.Clear();
            }
        }

        public void SetEmptyStage()
        {
            GameObject s = new GameObject();
            s.name = "EmptyStage";
            s.transform.parent = this.transform;
            s.transform.localPosition = Vector3.zero;
            s.transform.localRotation = Quaternion.identity;

            _currentStage = s.AddComponent<EmptyStage>();
        }

        public void InitClient()
        {
            SteamDebug.Log("initiating client");

            _client = new Client.Client(this);
        }

        public Coroutine RunRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }
    }
}