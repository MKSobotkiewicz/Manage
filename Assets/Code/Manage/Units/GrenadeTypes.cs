using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Units
{
    public static class GrenadeTypes
    {
        public enum EGrenadeType
        {
            FragGrenade
        }

        public static GrenadeType ToGrenadeType(EGrenadeType grenadeType)
        {
            switch (grenadeType)
            {
                case EGrenadeType.FragGrenade:
                    return FragGrenade;
                default:
                    return null;
            }
        }
        
        public static GrenadeType FragGrenade = new GrenadeType(
            "Frag Grenade",
            "Basic grenade type, have decent throw range, damage and damage radius.",
            "Grenades/Grenade",
            "Weapons/AR1_Assault_Rifle/textures/AR1_Icon",
            100,
            100,
            20);
    }
}
