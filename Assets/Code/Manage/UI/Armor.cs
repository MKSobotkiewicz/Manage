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
    public class Armor : Item
    {
        public Text ArmorName;
        public Text ArmorInfo;
        public Text ArmorArmor;
        public Text ArmorStamina;
        public Text ArmorEndurance;
        public Text ArmorMarksmanship;
        public Text ArmorCunning;
        public Text ArmorCharisma;

        private float infoTime;
        private static float timeToShowTooltip = 1;
        private bool entered = false;
        private bool showed = false;

        public new void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            base.Setup(itemType, _inventoryCanvas);
            ArmorName.text = itemType.Name;
            ArmorInfo.text = itemType.Info;
            ArmorArmor.text = (itemType as Units.ArmorType).Value.ToString();
            ArmorStamina.text = (itemType as Units.ArmorType).Stamina.ToString();
            ArmorEndurance.text = (itemType as Units.ArmorType).Endurance.ToString();
            ArmorMarksmanship.text = (itemType as Units.ArmorType).Marksmanship.ToString();
            ArmorCunning.text = (itemType as Units.ArmorType).Cunning.ToString();
            ArmorCharisma.text = (itemType as Units.ArmorType).Charisma.ToString();
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
                if ( !showed && !isDragged)
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
