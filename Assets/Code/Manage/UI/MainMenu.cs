﻿using System;
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
    public class MainMenu : MonoBehaviour
    {
        public Loading LoadingPrefab;

        public void StartNewCampaign()
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.1f).setOnComplete(LoadFirstScene).setEase(LeanTweenType.easeInCubic);
        }

        public void LoadGame()
        {
        }

        public void Settings()
        {
        }

        public void Credits()
        {
        }

        public void Exit()
        {
            ExitGame.Exit();
        }

        private void LoadFirstScene()
        {
            var loading=Instantiate(LoadingPrefab,transform.parent);
            var loadingScene =SceneManager.LoadSceneAsync("Resources/Scenes/Mission1", LoadSceneMode.Single);
            loading.LoadingScene = loadingScene;
        }
    }
}