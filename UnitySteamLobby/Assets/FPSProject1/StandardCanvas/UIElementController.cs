using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    [System.Serializable]
    public class UIElementController
    {
        public List<UIElementObj> uiElementObjects;

        private BaseStage _ownerStage = null;

        public void InitElementController(BaseStage ownerStage, Transform parent, List<UIElementType> uiElementTypes)
        {
            _ownerStage = ownerStage;
            uiElementObjects = new List<UIElementObj>();

            foreach(UIElementType t in uiElementTypes)
            {
                UIElementObj element = GameObject.Instantiate(ownerStage.INITIALIZER.RESOURCE_LOADER.uiElementLoader.GetLoadedObj(t)) as UIElementObj;
                uiElementObjects.Add(element);

                element.transform.SetParent(parent);
                element.transform.localPosition = Vector3.zero;
                element.transform.localRotation = Quaternion.identity;

                element.Init(ownerStage.INITIALIZER);
            }
        }

        public void OnFixedUpdate()
        {
            foreach(UIElementObj e in uiElementObjects)
            {
                e.OnFixedUpdate();
            }
        }

        public void OnUpdate()
        {
            foreach (UIElementObj e in uiElementObjects)
            {
                e.OnUpdate();
            }
        }

        public void OnLateUpdate()
        {
            foreach (UIElementObj e in uiElementObjects)
            {
                e.OnLateUpdate();
            }
        }
    }
}