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
        private Vector3 startVector;

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
            Destroy(gameObject);
        }
    }
}
