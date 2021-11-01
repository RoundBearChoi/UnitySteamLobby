using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.UI
{
    [System.Serializable]
    public class Menu
    {
        [SerializeField] protected UIMenu _uiMenu = null;
        [SerializeField] protected List<MenuItem> _listMenuItems;

        public virtual void Init(UIMenu uiSelectionMenu, Color selectedColor, Color unSelectedColor) { ClassDebug.Log("undefined"); }
        public virtual void OnFixedUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnUpdate() { ClassDebug.Log("undefined"); }
        public virtual void OnLateUpdate() { ClassDebug.Log("undefined"); }

        public virtual void SetMenuItems(UIMenu uiSelectionMenu, Color selectedColor, Color unSelectedColor)
        {
            _uiMenu = uiSelectionMenu;
            _listMenuItems = new List<MenuItem>();

            Transform[] arr = uiSelectionMenu.transform.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.name.Contains("SelectOption"))
                {
                    _listMenuItems.Add(new MenuItem_Standard(_uiMenu.INITIALIZER, selectedColor, unSelectedColor, t.gameObject));
                }
            }
        }
    }
}