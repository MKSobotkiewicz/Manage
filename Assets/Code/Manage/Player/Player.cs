using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Organizations;
using Manage.Units;

namespace Manage.Player
{
    public class Player : MonoBehaviour
    {
        public Organization Organization { get; private set; }

        public List<Unit> Units { get; private set; }
        public PlayerInventory PlayerInventory { get;private set;}
        
        public void Awake()
        {
            Organization = OrganizationTypes.Empire;
            PlayerInventory = new PlayerInventory();
            PlayerInventory.Add(WeaponTypes.Assault_Rifle);
            PlayerInventory.Add(WeaponTypes.Carbine);
            PlayerInventory.Add(ArmorTypes.MercenaryBlackArmor);
            PlayerInventory.Add(ArmorTypes.MercenaryOrangeArmor);
            PlayerInventory.Add(ArmorTypes.SoldierBlueArmor);
            PlayerInventory.Add(ArmorTypes.SoldierCamoAndJungleArmor);
            PlayerInventory.Add(ArmorTypes.SoldierJungleArmor);
            PlayerInventory.Add(HelmetTypes.CamouflagedCombatHelmet);
            PlayerInventory.Add(HelmetTypes.DesertCombatHelmet);
            PlayerInventory.Add(HelmetTypes.SteelHelmet);
            PlayerInventory.Add(VestTypes.CamouflagedCombatVest);
            PlayerInventory.Add(VestTypes.DesertCombatVest);
            PlayerInventory.Add(VestTypes.SteelVest);
        }
    }
}
