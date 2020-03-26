using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Manage.Settings
{
    public class Audio:MonoBehaviour
    {
        public AudioMixer MasterAudioMixer;

        public float MasterVolume { get; private set; }
        public float SoundEffectsVolume { get; private set; }
        public float MusicVolume { get; private set; }

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
            MasterAudioMixer.SetFloat("Master", MasterVolume);
        }

        public void SetSoundEffectsVolume(float soundEffectsVolume)
        {
            SoundEffectsVolume = soundEffectsVolume;
            PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolume);
            PlayerPrefs.Save();
            MasterAudioMixer.SetFloat("GameEffects", SoundEffectsVolume);
        }

        public void SetMusicVolume(float musicVolume)
        {
            MusicVolume = musicVolume;
            PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
            PlayerPrefs.Save();
            MasterAudioMixer.SetFloat("Music", MusicVolume);
        }
    }
}
