using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manage.Utilities;

namespace Manage.UI
{
    public class Loading : MonoBehaviour
    {
        public Text PercentageText;
        public Button ContinueButton;
        public Image LoadingPanel;
        public AsyncOperation LoadingSceneOperation;

        public bool Continue = false;

        private bool done = false;
        private Scene thisScene;

        public void Start()
        {
            thisScene = SceneManager.GetActiveScene();
        }

        public void Update()
        {
            if (LoadingSceneOperation == null)
            {
                return;
            }
            UpdatePercentage(LoadingSceneOperation.progress);
            if (LoadingSceneOperation.progress>=0.85f && done is false)
            {
                done = true;
                ContinueButton.gameObject.SetActive(true);
                LoadingPanel.gameObject.SetActive(false);
            }
        }

        public void Click()
        {
            LoadingSceneOperation.allowSceneActivation = true;
        }

        public void UpdatePercentage(float value)
        {
            PercentageText.text = ((int)(value*100)).ToString()+"%";
        }
    }
}