using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    [System.Serializable]
    public class MenuController
    {
        public UIMenu uiMenu = null;

        [SerializeField]
        private BaseStage _ownerStage = null;

        public void Init(BaseStage ownerStage, Transform parent, UIMenuType uiSelectionType)
        {
            _ownerStage = ownerStage;

            uiMenu = GameObject.Instantiate(
                _ownerStage.INITIALIZER.RESOURCE_LOADER.uiMenuLoader.GetLoadedObj(uiSelectionType) as UIMenu);

            uiMenu.transform.SetParent(parent);
            uiMenu.transform.localPosition = Vector3.zero;
            uiMenu.transform.localRotation = Quaternion.identity;

            uiMenu.Init(_ownerStage.INITIALIZER);
        }

        public void OnFixedUpdate()
        {
            uiMenu.OnFixedUpdate();
        }

        public void OnUpdate()
        {
            uiMenu.OnUpdate();
        }

        public void OnLateUpdate()
        {
            uiMenu.OnLateUpdate();
        }
    }
}