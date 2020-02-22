﻿using System.Collections.Generic;
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
                                  Vehicle vehicle,
                                  Character character,
                                  Transform transform)
        {
            var inventoryFactory = new InventoryFactory();
            GameObject go;
            if (vehicle == null)
            {
                go = inventoryFactory.PutOnArmor(armorType, transform, character.Gender);
            }
            else
            {
                go = inventoryFactory.SpawnVehicle(vehicle, transform);
            }
            go.transform.parent = null;
            var unit = go.GetComponent<Unit>();
            unit.Character = character;
            var children = unit.GetComponentsInChildren<Transform>();
            unit.Inventory.ArmGrenade(GrenadeTypes.FragGrenade);
            if (vehicle == null)
            {
                unit.Arm(weaponType);
                unit.PutOnHelmet(helmetType);
                unit.PutOnVest(vestType);
            }
            else
            {
                unit.Arm(vehicle.WeaponType);
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
