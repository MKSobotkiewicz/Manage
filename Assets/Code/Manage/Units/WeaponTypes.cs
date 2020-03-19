using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public static class WeaponTypes
    {
        public enum EWeaponType
        {
            Pistol,
            Machine_Pistol,
            Assault_Rifle,
            Carbine,
            Cannon_85mm,
            Gun_13mm
        }

        public static WeaponType ToWeaponType(EWeaponType weaponType)
        {
            switch (weaponType)
            {
                case EWeaponType.Pistol:
                    return Pistol;
                case EWeaponType.Machine_Pistol:
                    return Machine_Pistol;
                case EWeaponType.Assault_Rifle:
                    return Assault_Rifle;
                case EWeaponType.Carbine:
                    return Carbine;
                case EWeaponType.Cannon_85mm:
                    return Cannon_85mm;
                case EWeaponType.Gun_13mm:
                    return Gun_13mm;
                default:
                    return null;
            }
        }

        public static readonly WeaponType Pistol= new WeaponType(
            "Pistol",
            "Average 9x19mm pistol, good for self defense. Before collapse used by many law enforcement agencies.",
            "Weapons/Pistol/Block_Pistol",
            "Weapons/C15_Carbine/textures/C15_ItemIcon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            140,
            60,
            0.5,
            4,
            17,
            true,
            BulletTypes.BulletType_9x19mm);

        public static readonly WeaponType Machine_Pistol = new WeaponType(
            "Machine pistol",
            "Old straight blowback 9x19mm machine pistol, fitted with high capacity drum mag.",
            "Weapons/Machine_Pistol/Machine_Pistol",
            "Weapons/C15_Carbine/textures/C15_ItemIcon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            180,
            60,
            0.2,
            6,
            71,
            false,
            BulletTypes.BulletType_9x19mm);

        public static readonly WeaponType Assault_Rifle = new WeaponType(
            "Assault rifle",
            "Old but reliable construction, packs powerful 7.62x39mm bullets. Before the collapse was used mostly by second line units. Good at medium to long range.",
            "Weapons/AR1_Assault_Rifle/AK_103_Carbine",
            "Weapons/AR1_Assault_Rifle/textures/AR1_ItemIcon",
            "Weapons/AR1_Assault_Rifle/textures/AR1_Icon",
            80,
            100,
            0.5,
            5,
            30,
            false,
            BulletTypes.BulletType_7p62x39mm);

        public static readonly WeaponType Carbine = new WeaponType(
            "Carbine",
            "Standard carbine of the Imperial Army, now widespread among its ruins. Uses 5.56x45mm caliber bullets, better at medium range.",
            "Weapons/C15_Carbine/AUG_Carbine",
            "Weapons/C15_Carbine/textures/C15_ItemIcon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            140,
            90,
            0.2,
            5,
            30,
            false,
            BulletTypes.BulletType_5p56x45mm);

        public static readonly WeaponType Cannon_85mm = new WeaponType(
            "85mm Cannon",
            "Light tank gun.",
            "Weapons/85mm_Cannon/85mm_Cannon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            80,
            120,
            1,
            5,
            1,
            false,
            BulletTypes.ShellType_85mm);

        public static readonly WeaponType Gun_13mm = new WeaponType(
            "13mm Gun",
            "Heavy caliber mounted gun.",
            "Weapons/13mm_Gun/13mm_Gun",
            "Weapons/C15_Carbine/textures/C15_Icon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            80,
            120,
            0.5,
            10,
            100,
            false,
            BulletTypes.BulletType_13mm);
    }
}
