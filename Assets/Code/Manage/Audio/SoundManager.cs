using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public List<AudioClip> AudioClips=new List<AudioClip>();
        public bool PlayOnAwake=false;

        private AudioSource audioSource;

        protected static System.Random random = new System.Random();

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (PlayOnAwake)
            {
                Play();
            }
        }

        public void Play()
        {
            if (AudioClips.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(AudioClips[random.Next(0, AudioClips.Count-1)]);
        }
    }
}