using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Manage.Units;


namespace Manage.UI
{
    public class FloatingLifeTextPopup : MonoBehaviour
    {
        public void Begin(int value)
        {
            var text = GetComponent<Text>();
            var canvasGroup = GetComponent<CanvasGroup>();
            var rectTransform = GetComponent<RectTransform>();
            transform.SetParent(transform.parent.parent);

            if (value > 0)
            {
                text.text = "+" + value.ToString();
                text.color = new Color(0, 1, 0, 1);
            }
            else
            {
                text.text = value.ToString();
                text.color = new Color(1, 0, 0, 1);
            }
            LeanTween.alphaCanvas(canvasGroup, 0, 1);
            LeanTween.moveY(rectTransform, rectTransform.anchoredPosition.y+50, 1).setOnComplete(Destroy);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
