using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RB.UI
{
    public class StandardCanvas : MonoBehaviour
    {
        [Space(10)]
        [Header("Debug")]
        [SerializeField] BaseStage _ownerStage = null;
        [Space(10)] public MenuController menuController;
        [Space(10)] public UIElementController uiElementController;
        [SerializeField] float _scaleRatio = 1f;
        [SerializeField] float _prevHeight = 0f;
        CanvasScaler _canvasScaler = null;

        public void InitCanvas(BaseStage ownerStage, UIMenuType uiSelectionMenuType, List<UIElementType> uiElementTypes)
        {
            _ownerStage = ownerStage;
            menuController.Init(ownerStage, this.transform, uiSelectionMenuType);
            uiElementController.InitElementController(ownerStage, this.transform, uiElementTypes);

            _canvasScaler = this.gameObject.GetComponent<CanvasScaler>();
        }

        public void OnFixedUpdate()
        {
            menuController.OnFixedUpdate();
            uiElementController.OnFixedUpdate();
        }

        public void OnUpdate()
        {
            menuController.OnUpdate();
            uiElementController.OnUpdate();
        }

        public void OnLateUpdate()
        {
            menuController.OnLateUpdate();
            uiElementController.OnLateUpdate();

            UpdateScale();
        }

        void UpdateScale()
        {
            if (_prevHeight != Screen.height)
            {
                _prevHeight = Screen.height;
                _scaleRatio = Screen.height / 1080f;
                _canvasScaler.scaleFactor = _scaleRatio;

                GeneralDebug.Log("changing ui ratio to: " + _scaleRatio);
            }
        }
    }
}