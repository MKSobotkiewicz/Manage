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
    public class Helmet : Item
    {
        public Text HelmetName;
        public Text HelmetInfo;
        public Text HelmetArmor;
        public Text HelmetStamina;
        public Text HelmetEndurance;
        public Text HelmetMarksmanship;
        public Text HelmetCunning;
        public Text HelmetCharisma;

        private float infoTime;
        private static float timeToShowTooltip = 1;
        private bool entered = false;
        private bool showed = false;

        public new void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            base.Setup(itemType, _inventoryCanvas);
            HelmetName.text = itemType.Name;
            HelmetInfo.text = itemType.Info;
            HelmetArmor.text = (itemType as Units.HelmetType).Value.ToString();
            HelmetStamina.text = (itemType as Units.HelmetType).Stamina.ToString();
            HelmetEndurance.text = (itemType as Units.HelmetType).Endurance.ToString();
            HelmetMarksmanship.text = (itemType as Units.HelmetType).Marksmanship.ToString();
            HelmetCunning.text = (itemType as Units.HelmetType).Cunning.ToString();
            HelmetCharisma.text = (itemType as Units.HelmetType).Charisma.ToString();
        }

        public void Update()
        {
            if (!entered)
            {
                return;
            }

            if (infoTime < timeToShowTooltip)
            {
                infoTime += Time.fixedDeltaTime;
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
