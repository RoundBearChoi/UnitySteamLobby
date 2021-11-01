using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RB.Sound;

namespace RB
{
    public class MenuSound_Listener : IListener
    {
        IGameInitializer _initializer = null;
        [SerializeField] List<MenuSound> _listSounds;

        public bool REMOVE { get { return false; } set { } }

        public MenuSound_Listener(IGameInitializer initializer)
        {
            MessageDebug.Log("adding listener: menusound");
            _initializer = initializer;
            _listSounds = new List<MenuSound>();
            MessageHandler_Network.listeners.Add(this);
        }

        public void OnNotify(Message message)
        {
            if (message.MESSAGE_TYPE == MessageType.GAME_STARTING_COUNTDOWN)
            {
                int countdown = message.ints[0];

                MenuSound sound = GameObject.Instantiate(_initializer.RESOURCE_LOADER.menuSoundLoader.GetLoadedObj(MenuSoundType.TRANSMISSION)) as MenuSound;
                sound.InitSound(true);

                _listSounds.Add(sound);
            }
        }

        public void OnUpdate()
        {
            DeleteSound();
        }

        void DeleteSound()
        {
            foreach (MenuSound sound in _listSounds)
            {
                if (sound.DELETE_AFTER_PLAYING)
                {
                    if (!sound.AUDIO_SOURCE.isPlaying)
                    {
                        _listSounds.Remove(sound);
                        GameObject.Destroy(sound.gameObject);
                        break;
                    }
                }
            }
        }
    }
}