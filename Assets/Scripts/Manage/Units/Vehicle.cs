using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class Vehicle
    {
        public string PrefabPath { get; private set; }
        public float Speed { get; private set; }
        public int Armour { get; private set; }
        public int HitPoints { get; private set; }
        public WeaponType WeaponType { get; private set; }

        public Vehicle(float speed, int armour, int hitPoints, WeaponType weaponType,string prefabPath)
        {
            Speed = speed;
            Armour = armour;
            HitPoints = hitPoints;
            WeaponType = weaponType;
            PrefabPath = prefabPath;
        }

        public static GameObject Load(Vehicle vehicle,Transform transform)
        {
            var go = GameObject.Instantiate((UnityEngine.Resources.Load(vehicle.PrefabPath) as GameObject), transform);
            return go;
        }
    }
}
