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
    public class Vest : Item
    {
        public Text VestName;
        public Text VestInfo;
        public Text VestArmor;
        public Text VestStamina;
        public Text VestEndurance;
        public Text VestMarksmanship;
        public Text VestCommand;
        public Text VestCharisma;

        private float infoTime;
        private static float timeToShowTooltip = 1;
        private bool entered = false;
        private bool showed = false;

        public new void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            base.Setup(itemType, _inventoryCanvas);
            VestName.text = itemType.Name;
            VestInfo.text = itemType.Info;
            VestArmor.text = (itemType as Units.VestType).Value.ToString();
            VestStamina.text = (itemType as Units.VestType).Stamina.ToString();
            VestEndurance.text = (itemType as Units.VestType).Endurance.ToString();
            VestMarksmanship.text = (itemType as Units.VestType).Marksmanship.ToString();
            VestCommand.text = (itemType as Units.VestType).Command.ToString();
            VestCharisma.text = (itemType as Units.VestType).Charisma.ToString();
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
