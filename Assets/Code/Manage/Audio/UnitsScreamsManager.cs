using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage.Units;

namespace Manage.Audio
{
    public class UnitsScreamsManager : MonoBehaviour
    {
        public List<AudioClip> MaleScreamsClips = new List<AudioClip>();
        public List<AudioClip> FemaleScreamsClips = new List<AudioClip>();
        public bool PlayOnAwake = false;

        private AudioSource audioSource;

        private static System.Random random = new System.Random();

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
            var unit = GetComponentInParent<Unit>();
            if (unit != null)
            {
                if (unit.Character != null)
                {
                    if (audioSource.isPlaying)
                    {
                        return;
                    }
                    if (random.Next(0, 1) >= 1)
                    {
                        return;
                    }
                    switch (unit.Character.Gender)
                    {
                        case Characters.EGender.Other:
                        case Characters.EGender.Female:
                            PlayFemaleScream();
                            break;

                        case Characters.EGender.Male:
                            PlayMaleScream();
                            break;
                    }
                }
            }
        }

        private void PlayFemaleScream()
        {
            if (FemaleScreamsClips.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(FemaleScreamsClips[random.Next(0, FemaleScreamsClips.Count - 1)]);
        }

        private void PlayMaleScream()
        {
            if (MaleScreamsClips.Count <= 0)
            {
                return;
            }
            audioSource.PlayOneShot(MaleScreamsClips[random.Next(0, MaleScreamsClips.Count - 1)]);
        }
    }
}