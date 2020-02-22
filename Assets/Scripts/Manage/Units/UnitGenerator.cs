using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Manage.Player;

namespace Manage.Units
{
    public class UnitGenerator : MonoBehaviour
    {
        public Player.Player Player;
        //public AllUnitsList AllUnitsList;
        public EOrganization Organization;
        public bool IsVehicle;

        public int Count;
        public uint Level;

        private WeaponType AC1AssaultRifle = WeaponTypes.AC1_Assault_Rifle;
        private WeaponType C15Carbine = WeaponTypes.C15_Carbine;
        private ArmorType InfantryArmor = ArmorTypes.SoldierCamoArmor;
        private ArmorType MercenaryArmor = ArmorTypes.MercenaryOrangeArmor;
        private Vehicle MGS = VehicleTypes.MGS;
        private Vehicle Jeep = VehicleTypes.Jeep;

        private static readonly System.Random random = new System.Random();

        public void Start()
        {
            Generate();
        }

        public void Generate()
        {
            var unitFactory = new UnitFactory();
            Organizations.Organization organization;
            switch (Organization)
            {
                case EOrganization.Empire:
                    organization = Organizations.OrganizationsList.Empire;
                    break;
                case EOrganization.Bandits:
                    organization = Organizations.OrganizationsList.Bandits;
                    break;
                case EOrganization.Rebels:
                    organization = Organizations.OrganizationsList.Rebels;
                    break;
                default:
                    organization = null;
                    break;
            }
            if (!IsVehicle)
            {
                for (int i = 0; i < Count; i++)
                {
                    var character = new Characters.Character(Characters.CharacterCulturesList.BasicCulture, organization, Level);
                    WeaponType weaponType;
                    ArmorType armor;
                    var rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        weaponType = C15Carbine;
                    }
                    else
                    {
                        weaponType = AC1AssaultRifle;
                    }
                    rand = random.Next(0, 2);
                    if (rand == 0)
                    {
                        armor = InfantryArmor;
                    }
                    else
                    {
                        armor = MercenaryArmor;
                    }
                    var transformCopy = new GameObject().transform;
                    transformCopy.position = transform.position;
                    transformCopy.rotation = transform.rotation;
                    var unit = unitFactory.Create(Player, weaponType, armor,HelmetTypes.CamouflagedCombatHelmet,VestTypes.CamouflagedCombatVest, null, character, transformCopy);
                    transform.position += new Vector3(random.Next(-10, 10), 0, random.Next(-10, 10));
                    //AllUnitsList.AllUnits.Add(unit);
                }
                return;
            }
            var character2 = new Characters.Character(Characters.CharacterCulturesList.BasicCulture, organization,Level);
            var unit2 = unitFactory.Create(Player, C15Carbine, InfantryArmor, HelmetTypes.CamouflagedCombatHelmet, VestTypes.CamouflagedCombatVest, Jeep, character2, transform);
            //AllUnitsList.AllUnits.Add(unit2);
        }

        [System.Serializable]
        public class UnitBlueprint
        {
            public UnitType UnitType;
            public GameObject Female;
            public GameObject Male;
        }

        [System.Serializable]
        public class WeaponBlueprint
        {
            public WeaponType WeaponType;
            public GameObject Weapon;
        }

        public enum EOrganization
        {
            Empire,
            Bandits,
            Rebels
        }
    }
}