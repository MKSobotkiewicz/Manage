using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class ChestPopup:MonoBehaviour
    {
        public string Info;
        public Text Text;
        public bool Deleting = false;

        public void Start()
        {
            Text.text = Info;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.2f).setEase(LeanTweenType.easeInCubic);
        }

        public void Delete()
        {
            Deleting = true;
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.2f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
