using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class PopupManager:MonoBehaviour
    {
        public Popup PopupPrefab;
        [TextArea(15, 20)]
        public string PopupText="";
        public float Delay = 1;

        private Popup popup;

        public void Enter()
        {
            popup = Instantiate(PopupPrefab,UnityEngine.Camera.main.GetComponentInChildren<Canvas>().transform);
            popup.Setup(PopupText, Delay);
        }

        public void Exit()
        {
            popup.Delete();
        }
    }
}
