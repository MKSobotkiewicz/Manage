using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class Settings : MonoBehaviour
    {
        public Manage.Settings.Audio Audio;
        public Canvas GameSettingsCanvas;
        public GraphicSettings GraphicSettingsCanvas;
        public AudioSettings AudioSettingsCanvas;

        public void ShowAudioSettings()
        {
            AudioSettingsCanvas.Begin(Audio);

            GameSettingsCanvas.gameObject.SetActive(false);
            GraphicSettingsCanvas.gameObject.SetActive(false);
            AudioSettingsCanvas.gameObject.SetActive(true);
        }

        public void ShowGraphicSettings()
        {
            GameSettingsCanvas.gameObject.SetActive(false);
            GraphicSettingsCanvas.gameObject.SetActive(true);
            AudioSettingsCanvas.gameObject.SetActive(false);
        }

        public void ShowGameSettings()
        {
            GameSettingsCanvas.gameObject.SetActive(true);
            GraphicSettingsCanvas.gameObject.SetActive(false);
            AudioSettingsCanvas.gameObject.SetActive(false);
        }
    }
}