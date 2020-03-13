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
            AC1_Assault_Rifle,
            C15_Carbine,
            Cannon_85mm,
            Gun_13mm
        }

        public static WeaponType ToWeaponType(EWeaponType weaponType)
        {
            switch (weaponType)
            {
                case EWeaponType.AC1_Assault_Rifle:
                    return AC1_Assault_Rifle;
                case EWeaponType.C15_Carbine:
                    return C15_Carbine;
                case EWeaponType.Cannon_85mm:
                    return Cannon_85mm;
                case EWeaponType.Gun_13mm:
                    return Gun_13mm;
                default:
                    return null;
            }
        }

        public static readonly WeaponType AC1_Assault_Rifle = new WeaponType(
            "AR1 rifle",
            "Old but reliable construction, packs powerful 8x50mm bullets. Before the collapse was used mostly by second line units. Good at medium to long range.",
            "Weapons/AR1_Assault_Rifle/AR1_Assault",
            "Weapons/AR1_Assault_Rifle/textures/AR1_ItemIcon",
            "Weapons/AR1_Assault_Rifle/textures/AR1_Icon",
            80,
            100,
            0.5,
            5,
            20,
            BulletTypes.BulletType_8x50mm);
        public static readonly WeaponType C15_Carbine = new WeaponType(
            "C15 carbine",
            "Standard carbine of the Imperial Army, now widespread among its ruins. Uses 5.5x45mm caliber bullets, better at medium range.",
            "Weapons/C15_Carbine/C15_Carbine",
            "Weapons/C15_Carbine/textures/C15_ItemIcon",
            "Weapons/C15_Carbine/textures/C15_Icon",
            140,
            90,
            0.2,
            5,
            30,
            BulletTypes.BulletType_5p5x45mm);
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
            BulletTypes.BulletType_13mm);
    }
}
