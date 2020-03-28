using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Manage.Settings
{
    public class Audio:MonoBehaviour
    {
        public AudioMixer MasterAudioMixer;

        public float MasterVolume = 1;
        public float SoundEffectsVolume = 1;
        public float MusicVolume = 1;

        void OnLevelWasLoaded(int level)
        {
            MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
            SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume");
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume");

            MasterAudioMixer.SetFloat("Master", MasterVolume);
            MasterAudioMixer.SetFloat("GameEffects", SoundEffectsVolume);
            MasterAudioMixer.SetFloat("Music", MusicVolume);
        }

        public void SetMasterVolume(float masterVolume)
        {
            MasterVolume = masterVolume;
            PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
            PlayerPrefs.Save();
            MasterAudioMixer.SetFloat("MasterVolume", CalculateVolume(MasterVolume));
        }

        public void SetSoundEffectsVolume(float soundEffectsVolume)
        {
            SoundEffectsVolume = soundEffectsVolume;
            PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolume);
            PlayerPrefs.Save();
            MasterAudioMixer.SetFloat("GameEffectsVolume", CalculateVolume(SoundEffectsVolume));
        }

        public void SetMusicVolume(float musicVolume)
        {
            MusicVolume = musicVolume;
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            PlayerPrefs.Save();
            MasterAudioMixer.SetFloat("MusicVolume", CalculateVolume(MusicVolume));
        }

        private float CalculateVolume(float volume)
        {
            return (float)Math.Pow(volume,0.25) * 80 - 80;
        } 
    }
}
