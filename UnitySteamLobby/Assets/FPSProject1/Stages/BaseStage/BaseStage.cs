using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.GameElements;
using RB.UI;

namespace RB
{
    public abstract class BaseStage : MonoBehaviour
    {
        public IGameInitializer INITIALIZER { get { return _initializer; } }

        [Space(10)]
        [Header("Debug")]
        [SerializeField] protected IGameInitializer _initializer = null;
        [SerializeField] protected StandardCanvas _standardCanvas = null;
        [SerializeField] protected List<GameElement> _listGameElements = null;
        protected IListener _listener = null;

        public abstract void InitGameElements();
        public abstract void InitStandardCanvas();

        public virtual void InitStage(IGameInitializer initializer)
        {
            _initializer = initializer;
            _listGameElements = new List<GameElement>();
        }

        public virtual void InstantiateGameElements(List<GameElementType> gameElementTypes)
        {
            foreach(GameElementType t in gameElementTypes)
            {
                GameElement g = GameObject.Instantiate(_initializer.RESOURCE_LOADER.gameElementLoader.GetLoadedObj(t)) as GameElement;
                g.transform.SetParent(this.transform, true);

                g.InitGameElement(_initializer);
                _listGameElements.Add(g);
            }
        }

        public virtual void AddGameElement(GameElement element)
        {
            _listGameElements.Add(element);
            element.transform.SetParent(this.transform, true);
        }

        public virtual void InstantiateStandardCanvas(UIMenuType uiSelectionMenuType, List<UIElementType> uiElementTypes)
        {
            _standardCanvas = Instantiate(_initializer.RESOURCE_LOADER.uiLoader.GetLoadedObj(UIType.STANDARD_CANVAS)) as StandardCanvas;
            _standardCanvas.transform.SetParent(this.transform, true);
            _standardCanvas.InitCanvas(this, uiSelectionMenuType, uiElementTypes);
        }

        public virtual void OnEnter() { ClassDebug.Log("undefined"); }
        public virtual void OnExit() { ClassDebug.Log("stage onexit.. undefined.."); }
        public virtual void OnFixedUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnLateUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnESC() { ClassDebug.Log("pressed escape.. action undefined.."); }

        private void OnDestroy()
        {
            if (_listener != null)
            {
                _listener.REMOVE = true;
            }
        }
    }
}