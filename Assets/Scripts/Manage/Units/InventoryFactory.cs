using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class InventoryFactory
    {
        public InventoryFactory()
        {
        }

        public GameObject PutOnArmor(ArmorType armorType, Transform transform, Characters.EGender gender)
        {
            var go = ArmorType.Load(armorType, transform, gender);
            go.GetComponent<Unit>().Inventory = new Inventory();
            go.GetComponent<Unit>().Inventory.ArmorType = armorType;
            return go;
        }

        public GameObject SpawnVehicle(VehicleType vehicleType, Transform transform)
        {
            var go = VehicleType.Load(vehicleType, transform);
            go.GetComponent<Unit>().Inventory = new Inventory();
            go.GetComponent<Unit>().Inventory.VehicleType = vehicleType;
            return go;
        }
    }
}
