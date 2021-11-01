using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Sound
{
    public class MenuSound : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource = null;
        [SerializeField] bool _deleteAfterPlaying = false;

        public AudioSource AUDIO_SOURCE { get { return _audioSource; } }
        public bool DELETE_AFTER_PLAYING { get { return _deleteAfterPlaying; } }

        public virtual void InitSound(bool deleteAfterPlaying)
        {
            _audioSource = this.gameObject.GetComponent<AudioSource>();
            _deleteAfterPlaying = deleteAfterPlaying;
        }
    }
}