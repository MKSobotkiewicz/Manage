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
    public class Weapon : Item
    {
        public Text WeaponName;
        public Text WeaponInfo;
        public Text WeaponDamage;
        public Text WeaponPiercing;
        public Text WeaponSpread;
        public Text WeaponRange;
        public Text WeaponAmmo;
        public Text WeaponRof;

        private float infoTime;
        private static float timeToShowTooltip = 1;
        private bool entered = false;
        private bool showed = false;

        public new void Setup(ItemType itemType, Canvas _inventoryCanvas)
        {
            base.Setup(itemType, _inventoryCanvas);
            WeaponName.text = itemType.Name;
            WeaponInfo.text = itemType.Info;
            WeaponDamage.text = (itemType as WeaponType).BulletType.Damage.ToString();
            WeaponPiercing.text = (itemType as WeaponType).BulletType.Piercing.ToString();
            WeaponSpread.text = (itemType as WeaponType).Spread.ToString();
            WeaponRange.text = (itemType as WeaponType).MaxRange.ToString();
            WeaponAmmo.text = (itemType as WeaponType).Ammo.ToString();
            WeaponRof.text = (1 / (itemType as WeaponType).AimingTime).ToString();
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
                    InfoPanel.transform.localScale = new Vector3(0, 0, 0);
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
