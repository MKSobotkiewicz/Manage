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
        public Grenade Grenade { get; private set; }
        public Vehicle Vehicle { get; set; }
        public ArmorType ArmorType { get; set; }
        public Vest Vest { get; private set; }
        public Helmet Helmet { get; private set; }

        public Inventory()
        {
            Weapon = null;
            Grenade = null;
            ArmorType = null;
            Vest = null;
            Helmet = null;
        }

        public int GetArmor()
        {
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
            var cunning = ArmorType.Cunning;
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
            if (Weapon != null)
            {
                ThrowAway(Weapon);
            }
            Weapon = weapon;
            return true;
        }

        public bool Arm(WeaponType weaponType,Transform transform)
        {
            UnityEngine.Debug.Log("Arming handgun");
            if (Weapon != null)
            {
                ThrowAway(Weapon);
            }
            Weapon = Weapon.Create(weaponType, transform);
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
            Weapon = Weapon.Create(weaponType, transform);
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
            Vest = Vest.Create(vestType, transform, gender);
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
            Helmet = helmet;
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
            Helmet = Helmet.Create(helmetType, transform);
            return returnedHelmetType;
        }

        public bool ArmGrenade(Grenade grenade)
        {
            UnityEngine.Debug.Log("Arming grenade");
            if (Grenade != null)
            {
                ThrowAway(Grenade);
            }
            Grenade = grenade;
            return true;
        }

        public Inventory ThrowAway(Weapon weapon)
        {
            return this;
        }

        public Inventory ThrowAway(Grenade grenade)
        {
            return this;
        }
    }
}
