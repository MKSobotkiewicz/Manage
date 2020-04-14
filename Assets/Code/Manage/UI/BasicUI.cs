using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Units;

namespace Manage.UI
{
    public class BasicUI : MonoBehaviour
    {
        public float Delay = 0;

        private Vector3 startVector;
        private bool began = false;

        public void Start()
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

        public void Update()
        {
            if (Delay > 0)
            {
                Delay -= Time.deltaTime;
            }
            if (Delay <= 0&& began is false)
            {
                Begin();
                began = true;
            }
        }

        public void Begin()
        {
            transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.1f).setEase(LeanTweenType.easeInCubic);
        }

        public void Click()
        {
            UnityEngine.Debug.Log("CLICK");
            startVector = Input.mousePosition- GetComponent<RectTransform>().localPosition;
        }

        public void Drag()
        {
            UnityEngine.Debug.Log("DRAG");
            GetComponent<RectTransform>().localPosition = Input.mousePosition - startVector;
        }

        public void ResetPosition()
        {
            UnityEngine.Debug.Log("RESET POSITION");
            GetComponent<RectTransform>().localPosition =new Vector3(0,0,0);
        }

        public void Delete()
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.1f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
