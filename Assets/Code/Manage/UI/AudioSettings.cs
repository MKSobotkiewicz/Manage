using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class AudioSettings : MonoBehaviour
    {
        public Manage.Settings.Audio Audio;
        public Slider MasterVolumeSlider;
        public Slider MusicVolumeSlider;
        public Slider EffectsVolumeSlider;

        public void Start()
        {
            MasterVolumeSlider.value = Audio.MasterVolume;
            MusicVolumeSlider.value = Audio.MusicVolume;
            EffectsVolumeSlider.value = Audio.SoundEffectsVolume;

            MasterVolumeSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
            MusicVolumeSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
            EffectsVolumeSlider.onValueChanged.AddListener(delegate { SetEffectsVolume(); });
        }

        public void SetMasterVolume()
        {
            Audio.SetMasterVolume(MasterVolumeSlider.value);
        }

        public void SetMusicVolume()
        {
            Audio.SetMusicVolume(MusicVolumeSlider.value);
        }

        public void SetEffectsVolume()
        {
            Audio.SetSoundEffectsVolume(EffectsVolumeSlider.value);
        }
    }
}
