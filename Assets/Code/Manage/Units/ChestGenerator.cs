using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class ChestGenerator:MonoBehaviour
    {
        public List<Item> items=new List<Item>();

        private static readonly string chestPrefabPath = "Chests/Chest";

        public void Start()
        {
            var prefab = UnityEngine.Resources.Load(chestPrefabPath);
            var go = Instantiate(prefab, transform) as GameObject;
            go.transform.parent = null;
            var chest = go.GetComponent<Chest>();
            foreach (var item in items)
            {
                switch (item.ItemType)
                {
                    case ItemType.EitemType.Armor:
                        chest.itemTypes.Add(ArmorTypes.ToArmorType(item.ArmorType));
                        break;
                    case ItemType.EitemType.Vest:
                        chest.itemTypes.Add(VestTypes.ToVestType(item.VestType));
                        break;
                    case ItemType.EitemType.Helmet:
                        chest.itemTypes.Add(HelmetTypes.ToHelmetType(item.HelmetType));
                        break;
                    case ItemType.EitemType.Weapon:
                        chest.itemTypes.Add(WeaponTypes.ToWeaponType(item.WeaponType));
                        break;
                    case ItemType.EitemType.Grenade:
                        chest.itemTypes.Add(GrenadeTypes.ToGrenadeType(item.GrenadeType));
                        break;
                }
            }
            Destroy(gameObject);
        }

        [System.Serializable]
        public class Item
        {
            public ItemType.EitemType ItemType;
            public VestTypes.EVestType VestType;
            public HelmetTypes.EHelmetType HelmetType;
            public ArmorTypes.EArmorType ArmorType;
            public WeaponTypes.EWeaponType WeaponType;
            public GrenadeTypes.EGrenadeType GrenadeType;
        }
    }
}
