using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public class Inventory
    {
        public Weapon Weapon { get; private set; }
        public GrenadeType GrenadeType { get; private set; }
        public VehicleType VehicleType { get; set; }
        public ArmorType ArmorType { get; set; }
        public Vest Vest { get; private set; }
        public Helmet Helmet { get; private set; }

        private readonly string chestPrefabPath = "Chests/Chest";

        private static readonly System.Random random = new System.Random();

        public Inventory()
        {
            Weapon = null;
            GrenadeType = null;
            ArmorType = null;
            Vest = null;
            Helmet = null;
        }

        public int GetArmor()
        {
            if (VehicleType != null)
            {
                return VehicleType.Armour;
            }
            var value = ArmorType.Value;
            if (Vest != null)
            {
                value += Vest.VestType.Value;
            }
            if (Helmet != null)
            {
                value += Helmet.HelmetType.Value;
            }
            return value;
        }
        
        public uint GetStamina()
        {
            var stamina = ArmorType.Stamina;
            if (Vest != null)
            {
                stamina += Vest.VestType.Stamina;
            }
            if (Helmet != null)
            {
                stamina += Helmet.HelmetType.Stamina;
            }
            return stamina;
        }

        public uint GetEndurance()
        {
            var endurance = ArmorType.Endurance;
            if (Vest != null)
            {
                endurance += Vest.VestType.Endurance;
            }
            if (Helmet != null)
            {
                endurance += Helmet.HelmetType.Endurance;
            }
            return endurance;
        }

        public uint GetMarksmanship()
        {
            var marksmanship = ArmorType.Marksmanship;
            if (Vest != null)
            {
                marksmanship += Vest.VestType.Marksmanship;
            }
            if (Helmet != null)
            {
                marksmanship += Helmet.HelmetType.Marksmanship;
            }
            return marksmanship;
        }

        public uint GetCunning()
        {
            uint cunning = 0;
            if (ArmorType != null)
            {
                cunning += ArmorType.Cunning;
            }
            if (Vest != null)
            {
                cunning += Vest.VestType.Cunning;
            }
            if (Helmet != null)
            {
                cunning += Helmet.HelmetType.Cunning;
            }
            return cunning;
        }

        public uint GetCharisma()
        {
            var charisma = ArmorType.Charisma;
            if (Vest != null)
            {
                charisma += Vest.VestType.Charisma;
            }
            if (Helmet != null)
            {
                charisma += Helmet.HelmetType.Charisma;
            }
            return charisma;
        }

        public bool Arm(Weapon weapon)
        {
            UnityEngine.Debug.Log("Arming handgun");
            Weapon = weapon;
            return true;
        }

        public bool Arm(WeaponType weaponType,Transform transform)
        {
            UnityEngine.Debug.Log("Arming handgun");
            if (weaponType != null)
            {
                Weapon = Weapon.Create(weaponType, transform);
            }
            return true;
        }

        public WeaponType Rearm(WeaponType weaponType, Transform transform)
        {
            UnityEngine.Debug.Log("Rearming handgun");
            WeaponType returnedWeaponType=null;
            if (Weapon != null)
            {
                returnedWeaponType=Weapon.WeaponType;
                Weapon.Destroy();
            }
            if (weaponType != null)
            {
                Weapon = Weapon.Create(weaponType, transform);
            }
            return returnedWeaponType;
        }

        public WeaponType Rearm(Weapon weapon)
        {
            UnityEngine.Debug.Log("Rearming handgun");
            WeaponType returnedWeaponType = null;
            if (Weapon != null)
            {
                returnedWeaponType = Weapon.WeaponType;
                Weapon.Destroy();
            }
            Weapon = weapon;
            return returnedWeaponType;
        }

        public VestType PutOnVest(Vest vest)
        {
            UnityEngine.Debug.Log("Putting on vest");
            VestType returnedVestType = null;
            if (Vest != null)
            {
                returnedVestType = Vest.VestType;
                Vest.Destroy();
            }
            Vest = vest;
            return returnedVestType;
        }

        public VestType PutOnVest(VestType vestType, Transform transform, Characters.EGender gender)
        {
            UnityEngine.Debug.Log("Putting on vest");
            VestType returnedVestType = null;
            if (Vest != null)
            {
                returnedVestType = Vest.VestType;
                Vest.Destroy();
            }
            if (vestType != null)
            {
                Vest = Vest.Create(vestType, transform, gender);
            }
            return returnedVestType;
        }

        public HelmetType PutOnHelmet(Helmet helmet)
        {
            UnityEngine.Debug.Log("Putting on helmet");
            HelmetType returnedHelmetType = null;
            if (Helmet != null)
            {
                returnedHelmetType = Helmet.HelmetType;
                Helmet.Destroy();
            }
            return returnedHelmetType;
        }

        public HelmetType PutOnHelmet(HelmetType helmetType, Transform transform)
        {
            UnityEngine.Debug.Log("Putting on helmet");
            HelmetType returnedHelmetType = null;
            if (Helmet != null)
            {
                returnedHelmetType = Helmet.HelmetType;
                Helmet.Destroy();
        }
        if (helmetType != null)
        {
            Helmet = Helmet.Create(helmetType, transform);
        }
            return returnedHelmetType;
        }

        public bool ArmGrenade(GrenadeType grenade)
        {
            UnityEngine.Debug.Log("Arming grenade");
            GrenadeType = grenade;
            return true;
        }

        public void Drop(Transform transform)
        {
            var items = new List<ItemType>();
            if (Weapon != null)
            {
                items.Add(Weapon.WeaponType);
            }
            if (GrenadeType != null)
            {
                items.Add(GrenadeType);
            }
            if (ArmorType != null)
            {
                items.Add(ArmorType);
            }
            if (Vest != null)
            {
                items.Add(Vest.VestType);
            }
            if (Helmet != null)
            {
                items.Add(Helmet.HelmetType);
            }
            if (items.Count > 0)
            {
                Chest chest=null;
                foreach (var otherChest in Chest.AllChests)
                {
                    if (Vector3.Distance(otherChest.transform.position,transform.position)<10)
                    {
                        chest = otherChest;
                    }
                }
                if (chest is null)
                {
                    var prefab = UnityEngine.Resources.Load(chestPrefabPath);
                    var go = GameObject.Instantiate(prefab, transform) as GameObject;
                    go.transform.parent = null;
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 2, go.transform.position.z);
                    chest = go.GetComponent<Chest>();
                    var rigidbody = go.GetComponent<Rigidbody>();
                    rigidbody.AddRelativeForce(new Vector3(random.Next(-20, 20), random.Next(20, 40), random.Next(-20, 20)), ForceMode.Impulse);
                    rigidbody.AddRelativeTorque(new Vector3(random.Next(-40, 40), random.Next(-40, 40), random.Next(-40, 40)), ForceMode.Impulse);
                }
                chest.itemTypes.AddRange(items);
            }
        }
    }
}
