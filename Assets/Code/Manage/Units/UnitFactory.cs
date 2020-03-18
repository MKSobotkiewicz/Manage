using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;

namespace Manage.Units
{
    public class UnitFactory
    {
        private static System.Random random = new System.Random();

        public UnitFactory()
        {
        }

        public Unit Create(Player.Player player,
                           WeaponType weaponType,
                           ArmorType armorType,
                           HelmetType helmetType,
                           VestType vestType,
                           GrenadeType grenadeType,
                           VehicleType vehicleType,
                           Character character,
                           Transform transform)
        {
            var inventoryFactory = new InventoryFactory();
            GameObject go;
            if (vehicleType == null)
            {
                go = inventoryFactory.PutOnArmor(armorType, transform, character.Gender);
            }
            else
            {
                go = inventoryFactory.SpawnVehicle(vehicleType, transform);
            }
            go.transform.parent = null;
            var unit = go.GetComponent<Unit>();
            unit.Character = character;
            var children = unit.GetComponentsInChildren<Transform>();
            unit.Inventory.ArmGrenade(grenadeType);
            if (vehicleType == null)
            {
                unit.PutOnHelmet(helmetType);
                unit.PutOnVest(vestType);
                unit.Rearm(weaponType);
            }
            else
            {
                unit.Arm(vehicleType.WeaponType);
            }
            foreach (var child in children)
            {
                if (child.name == "Flag")
                {
                    child.GetComponent<RawImage>().texture = character.Organization.Flag;
                }
            }
            if (player != null)
            {
                if (character.Organization == player.Organization)
                {
                    unit.Player = true;
                }
                else
                {
                    unit.Player = false;
                }
            }
            else
            {
                unit.Player = false;
            }

            AllUnitsList.Units.Add(unit);
            return unit;
        }
    }
}
