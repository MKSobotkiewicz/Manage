﻿using System;
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
    public class ItemSlot:MonoBehaviour
    {
        public EItemType ItemType;
        public Canvas Slot;
        Item item;

        public void RemoveItem()
        {
            if (item is Weapon)
            {
                GetComponentInParent<CharacterId>().Rearm(null);
            }
            else if (item is Armor)
            {
                GetComponentInParent<CharacterId>().ChangeArmor(null);
            }
            else if (item is Helmet)
            {
                GetComponentInParent<CharacterId>().ChangeHelmet(null);
            }
            else if (item is Vest)
            {
                GetComponentInParent<CharacterId>().ChangeVest(null);
            }
            if (item is Grenade)
            {
                GetComponentInParent<CharacterId>().ChangeGrenade(null);
            }
            item = null;
        }

        public bool PutItem(Item _item)
        {
            if (!(_item is Weapon && ItemType is EItemType.Weapon) &&
                !(_item is Armor && ItemType is EItemType.Armor) &&
                !(_item is Helmet && ItemType is EItemType.Helmet) &&
                !(_item is Vest && ItemType is EItemType.Vest)&&
                !(_item is Grenade && ItemType is EItemType.Grenade))
            {
                return false;
            }
            if (item != null)
            {
                item.PutInInventoryCanvas();
            }
            item = _item;
            item.transform.SetParent(Slot.transform);
            item.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            if (item is Weapon)
            {
                GetComponentInParent<CharacterId>().Rearm(item.ItemType as WeaponType);
            }
            else if (item is Armor)
            {
                GetComponentInParent<CharacterId>().ChangeArmor(item.ItemType as ArmorType);
            }
            else if (item is Helmet)
            {
                GetComponentInParent<CharacterId>().ChangeHelmet(item.ItemType as HelmetType);
            }
            else if (item is Vest)
            {
                GetComponentInParent<CharacterId>().ChangeVest(item.ItemType as VestType);
            }

            else if (item is Grenade)
            {
                GetComponentInParent<CharacterId>().ChangeGrenade(item.ItemType as GrenadeType);
            }
            return true;
        }

        public enum EItemType
        {
            Weapon,
            Armor,
            Helmet,
            Vest,
            Grenade
        }
    }
}
