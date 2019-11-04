using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project
{
    public class SoundManager : MonoBehaviour
    {
        #region ------------------------------dependencies
        AudioSource _audioSource;
        #endregion

        #region ------------------------------interface
        public void PlayAudioClip(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        #endregion
    }
}
