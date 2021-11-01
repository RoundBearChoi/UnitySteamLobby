using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    [System.Serializable]
    public class ResourceLoader
    {
        [SerializeField]
        IGameInitializer _initializer = null;

        public GameElementLoader gameElementLoader = null;
        public MenuSoundLoader menuSoundLoader = null;
        public UILoader uiLoader = null;
        public UIMenuLoader uiMenuLoader = null;
        public UIElementLoader uiElementLoader = null;
        public etcLoader etcLoader = null;

        public void Init(IGameInitializer initializer)
        {
            _initializer = initializer;

            gameElementLoader = InstantiateLoader<GameElementLoader>("GameElementLoader");
            menuSoundLoader = InstantiateLoader<MenuSoundLoader>("MenuSoundLoader");
            uiLoader = InstantiateLoader<UILoader>("UILoader");
            uiMenuLoader = InstantiateLoader<UIMenuLoader>("UIMenuLoader");
            uiElementLoader = InstantiateLoader<UIElementLoader>("UIElementLoader");
            etcLoader = InstantiateLoader<etcLoader>("etcLoader");

            gameElementLoader.InitLoader();
            menuSoundLoader.InitLoader();
            uiLoader.InitLoader();
            uiMenuLoader.InitLoader();
            uiElementLoader.InitLoader();
            etcLoader.InitLoader();
        }

        public T InstantiateLoader<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = new GameObject();
            obj.name = name;
            T component = obj.AddComponent<T>();

            obj.transform.parent = _initializer.TRANSFORM;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            return component;
        }
    }
}