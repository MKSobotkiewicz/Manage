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
    public class Grenade : Item
    {
        public Text GrenadeName;
        public Text GrenadeInfo;
        public Text GrenadeDamage;
        public Text GrenadeRadius;
        public Text GrenadeRange;

        private float infoTime;
        private static float timeToShowTooltip = 1;
        private bool entered = false;
        private bool showed = false;

        public new void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            base.Setup(itemType, _inventoryCanvas);
            GrenadeName.text = itemType.Name;
            GrenadeInfo.text = itemType.Info;
            GrenadeDamage.text = (itemType as Units.GrenadeType).Damage.ToString();
            GrenadeRadius.text = (itemType as Units.GrenadeType).Radius.ToString();
            GrenadeRange.text = (itemType as Units.GrenadeType).Range.ToString();
        }

        public void Update()
        {
            if (!entered)
            {
                return;
            }

            if (infoTime < timeToShowTooltip)
            {
                infoTime += Time.deltaTime;
            }
            else
            {
                if (!showed && !isDragged)
                {
                    showed = true;
                    var position = InfoPanel.transform.position;
                    InfoPanel.transform.SetParent(transform.parent.parent);
                    InfoPanel.transform.position = position;
                    InfoPanel.gameObject.SetActive(true);
                    InfoPanel.enabled = true;
                    LeanTween.scale(InfoPanel.gameObject, new Vector3(1, 1, 1), 0.1f).setEase(LeanTweenType.easeInCubic);
                }
            }
        }

        public void ShowInfo()
        {
            entered = true;
        }

        public void HideInfo()
        {
            LeanTween.scale(InfoPanel.gameObject, new Vector3(0, 0, 0), 0.1f).setOnComplete(Hide).setEase(LeanTweenType.easeInCubic);
        }

        private void Hide()
        {
            InfoPanel.transform.SetParent(transform);
            InfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(50, 25, 0);
            entered = false;
            showed = false;
            infoTime = 0;
            InfoPanel.gameObject.SetActive(false);
        }
    }
}
