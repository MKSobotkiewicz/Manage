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
        public AsyncOperation LoadingScene;

        public void Update()
        {
            if (LoadingScene == null)
            {
                return;
            }
            UpdatePercentage(LoadingScene.progress);
        }

        public void UpdatePercentage(float value)
        {
            PercentageText.text = (value*100).ToString()+"%";
        }
    }
}