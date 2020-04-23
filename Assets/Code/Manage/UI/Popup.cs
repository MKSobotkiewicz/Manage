using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class Popup:MonoBehaviour
    {
        public AudioClip ClipOnStart;
        public AudioClip ClipOnStop;

        private string popupText="Error";
        private float delay = 1;
        private Text text;
        private bool began = false;

        private RectTransform rectTransform;
        private AudioSource audioSource;

        public void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            audioSource= GetComponent<AudioSource>();
            text = GetComponentInChildren<Text>();
            text.text = popupText;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

        public void Setup(string _popupText, float _delay)
        {
            popupText = _popupText;
            delay = _delay;
        }

        public void Update()
        {
            rectTransform.anchoredPosition = Input.mousePosition;
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            if (delay <= 0 && began is false)
            {
                Begin();
                began = true;
            }
        }

        public void Begin()
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(ClipOnStart);
            }
            transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.2f).setEase(LeanTweenType.easeInCubic);
        }

        public void Delete()
        {
            if (audioSource != null&& began)
            {
                audioSource.PlayOneShot(ClipOnStop);
            }
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.2f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
