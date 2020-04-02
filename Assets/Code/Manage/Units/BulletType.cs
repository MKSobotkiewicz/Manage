using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public class BulletType
    {
        public string PrefabPath { get; private set; }
        public string MuzzleFlashPrefabPath { get; private set; }
        public double Velocity { get; private set; }
        public double Mass { get; private set; }
        public int Damage { get; private set; }
        public int Piercing { get; private set; }
        public bool IsAreaAttack { get; set; }
        public double AreaSize { get; set; }

        public BulletType(string prefab, 
                          string muzzleFlashPrefabPath,
                          double velocity,
                          double mass, 
                          int damage, 
                          int piercing) : this(prefab,
                                               muzzleFlashPrefabPath,
                                               velocity, 
                                               mass, 
                                               damage,
                                               piercing, 
                                               false, 
                                               0)
        {
        }

        public BulletType(string prefab,
                          string muzzleFlashPrefabPath,
                          double velocity,
                          double mass, 
                          int damage, 
                          int piercing, 
                          double areaSize) : this(prefab,
                                                  muzzleFlashPrefabPath,
                                                  velocity,
                                                  mass, 
                                                  damage, 
                                                  piercing, 
                                                  true,
                                                  areaSize)
        {
        }

        private BulletType(string prefab,
                           string muzzleFlashPrefabPath,
                           double velocity,
                           double mass,
                           int damage,
                           int piercing,
                           bool isAreaAttack,
                           double areaSize)
        {
            PrefabPath = prefab;
            MuzzleFlashPrefabPath = muzzleFlashPrefabPath;
            Velocity = velocity;
            Mass = mass;
            Damage = damage;
            Piercing = piercing;
            IsAreaAttack = isAreaAttack;
            AreaSize = areaSize;
        }

        public Bullet CreateBullet(Unit unit, Vector3 position,Vector3 target)
        {
            return Bullet.Create(this, unit, position, target);
        }
    }
}
