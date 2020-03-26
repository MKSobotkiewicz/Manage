using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class GraphicSettings : MonoBehaviour
    {
        public Dropdown ResolutionDropdown;
        public Dropdown SettingsDropdown;
        public Toggle WindowedToggle;

        public FullScreenMode FullScreenMode { get; private set; }

        void Start()
        {
            FullScreenMode = FullScreenMode.ExclusiveFullScreen;
            ResolutionDropdown.options = new List<Dropdown.OptionData>();
            int i = 0, current = 0;
            foreach (var resolution in Screen.resolutions)
            {
                ResolutionDropdown.options.Add(new Dropdown.OptionData(resolution.width + "x" + resolution.height + " : " + resolution.refreshRate));
                if (resolution.width == Screen.currentResolution.width &&
                    resolution.height == Screen.currentResolution.height &&
                    resolution.refreshRate == Screen.currentResolution.refreshRate)
                {
                    current = i;
                }
                i++;
            }
            ResolutionDropdown.value = current;

            ResolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(); });

            if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                WindowedToggle.isOn = true;
            }
            else
            {
                WindowedToggle.isOn = false;
            }

            WindowedToggle.onValueChanged.AddListener(delegate { ChangeResolution(); });

            SettingsDropdown.options = new List<Dropdown.OptionData>();
            SettingsDropdown.options.Add(new Dropdown.OptionData("VERY LOW"));
            SettingsDropdown.options.Add(new Dropdown.OptionData("LOW"));
            SettingsDropdown.options.Add(new Dropdown.OptionData("MEDIUM"));
            SettingsDropdown.options.Add(new Dropdown.OptionData("HIGH"));
            SettingsDropdown.options.Add(new Dropdown.OptionData("VERY HIGH"));
            SettingsDropdown.options.Add(new Dropdown.OptionData("ULTRA"));
            SettingsDropdown.value = QualitySettings.GetQualityLevel();

            SettingsDropdown.onValueChanged.AddListener(delegate { SelectSettings(); });
        }

        void SelectSettings()
        {
            QualitySettings.SetQualityLevel(SettingsDropdown.value);
        }

        void ChangeResolution()
        {
            if (WindowedToggle.isOn)
            {
                FullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                FullScreenMode = FullScreenMode.ExclusiveFullScreen;
            }
            Screen.SetResolution(Screen.resolutions[ResolutionDropdown.value].width,
                                 Screen.resolutions[ResolutionDropdown.value].height,
                                 FullScreenMode,
                                 Screen.resolutions[ResolutionDropdown.value].refreshRate);
        }
    }
}