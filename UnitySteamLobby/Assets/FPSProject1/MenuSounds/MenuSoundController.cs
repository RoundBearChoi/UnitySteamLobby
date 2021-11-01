using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Sound
{
    [System.Serializable]
    public class MenuSoundController
    {
        IGameInitializer _initializer = null;
        MenuSound_Listener _menuSoundListener = null;

        public MenuSoundController(IGameInitializer initializer)
        {
            _initializer = initializer;
            _menuSoundListener = new MenuSound_Listener(initializer);
        }

        public void OnUpdate()
        {
            _menuSoundListener.OnUpdate();
        }
    }
}