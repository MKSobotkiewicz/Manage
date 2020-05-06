using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public List<AudioClip> Tracks;
        public bool PlayOnAwake = false;

        private AudioSource audioSource;

        protected static System.Random random = new System.Random();

        void Awake()
        {
            Tracks = new List<AudioClip>();
            foreach (var track in UnityEngine.Resources.LoadAll("Audio/MusicTracks", typeof (AudioClip)))
            {
                Tracks.Add(track as AudioClip);
            }
            audioSource = GetComponent<AudioSource>();
            if (PlayOnAwake)
            {
                Play();
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (!audioSource.isPlaying)
            {
                Play();
            }
        }

        public void Play()
        {
            if (Tracks.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(Tracks[random.Next(0, Tracks.Count - 1)]);
        }
    }
}