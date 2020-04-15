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
    public class FloatingLifePanel : MonoBehaviour
    {
        public Unit Unit;

        public RectTransform FloatingLifeBar;

        public FloatingLifeTextPopup FloatingLifeTextPopupPrefab;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private float timeSinceChange=5;
        private bool isVisible=false;

        private int lastValue = 0;

        public void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            LeanTween.alphaCanvas(canvasGroup, 0,1);
        }

        public void Update()
        {
            if (Unit is null)
            {
                Destroy();
            }
            Move();
            if(Unit.HitPoints()== Unit.GetMaxHitPoints()&& isVisible==true)
            {
                timeSinceChange -= Time.deltaTime;
                if (timeSinceChange <= 0)
                {
                    isVisible = false;

                    LeanTween.alphaCanvas(canvasGroup, 0, 1);
                }
            }
        }

        public void Move()
        {
            if (Unit != null)
            {
                rectTransform.localPosition = UnityEngine.Camera.main.WorldToScreenPoint(Unit.transform.position);
            }
        }

        public void SetHitPoints()
        {
            if (Unit.HitPoints() != Unit.GetMaxHitPoints())
            {
                LeanTween.alphaCanvas(canvasGroup, 1, 1);
                isVisible = true;
                timeSinceChange = 5;
            }
            var change = Unit.HitPoints() - lastValue;
            if (change > 5 || change < -5)
            {
                var fltp = Instantiate(FloatingLifeTextPopupPrefab, transform);
                fltp.Begin(Unit.HitPoints() - lastValue);
            }
            float value = (Mathf.Clamp((float)Unit.HitPoints(), 0, float.MaxValue) / (float)Unit.GetMaxHitPoints()) * 2;
            FloatingLifeBar.localScale = new Vector3(value / 2, 1, 1);
            if (Unit.HitPoints() <= 0)
            {
                Destroy();
            }
            lastValue = Unit.HitPoints();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
