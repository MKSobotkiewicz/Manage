using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Manage.Space;
using Manage.Characters;

namespace Manage.Units
{
    public class WeaponType:ItemType
    {
        public string PrefabPath { get; private set; }
        public double Spread { get; private set; }
        public double MaxRange { get; private set; }
        public double AimingTime { get; private set; }
        public double ReloadTime { get; private set; }
        public uint Ammo { get; private set; }
        public bool IsPistol { get; private set; }
        public BulletType BulletType { get; private set; }

        private GameObject muzzleFlash;

        private static readonly System.Random random = new System.Random();

        public WeaponType(string name,
                          string info,
                          string prefabPath,
                          string iconPath,
                          double spread,
                          double maxRange,
                          double aimingTime,
                          double reloadTime,
                          uint ammo,
                          bool isPistol,
                          BulletType bulletType):base(name,info, iconPath)
        {
            IsPistol  = isPistol;
            PrefabPath = prefabPath;
            MaxRange = maxRange;
            Spread = spread;
            AimingTime = aimingTime;
            ReloadTime = reloadTime;
            Ammo = ammo;
            BulletType = bulletType;
        }

        public EAttackStatus Attack(Unit attacking, Unit target, Weapon weapon, double timeSinceLastShot, double aimingTime, int ammo)
        {
            if (ammo <= 0)
            {
                return EAttackStatus.NoAmmo;
            }
            if (timeSinceLastShot< aimingTime*(1-0.25*attacking.Character.CharacterTraits.Contains(CharacterTraitsList.FastShooter)))
            {
                return EAttackStatus.Aiming;
            }
            var distance = Vector3.Distance(attacking.Position(), target.Position());
            if (!IsInRange(distance,attacking.Character))
            {
                UnityEngine.Debug.Log("Out Of Range");
                return EAttackStatus.OutOfRange;
            }
            var transforms = weapon.gameObject.GetComponentsInChildren<Transform>();
            foreach (var transform in transforms)
            {
                if (transform.gameObject.name == "Muzzle")
                {
                    if (!(muzzleFlash is null))
                    {
                        GameObject.Destroy(muzzleFlash);
                    }
                    muzzleFlash = GameObject.Instantiate(UnityEngine.Resources.Load(BulletType.MuzzleFlashPrefabPath) as GameObject);
                    muzzleFlash.transform.position = transform.position;
                    muzzleFlash.transform.rotation = transform.rotation;
                    muzzleFlash.transform.Rotate(0, 180, 0);
                    var bullet = BulletType.CreateBullet(attacking, transform, target.Position());
                    RotateBullet(bullet, attacking);
                }
            }
            return EAttackStatus.Attacking;
        }

        public bool IsInRange(float distance,Character character)
        {
            if (distance < MaxRange*(1+0.35*character.CharacterTraits.Contains(CharacterTraitsList.Perceptive)))
            {
                return true;
            }
            return false;
        }

        public Texture2D CombatIcon()
        {
            return GameObject.Instantiate(UnityEngine.Resources.Load<Texture2D>(IconPath)) as Texture2D;
        }

        private void RotateBullet(Bullet bullet, Unit attacking)
        {
            var spreadNormalized = Spread*(BulletType.Velocity/1000);
            double marksmanship;
            if (attacking.Inventory.ArmorType != null)
            {
                marksmanship = attacking.Character.CharacterStats.GetMarksmanship() + attacking.Inventory.ArmorType.Marksmanship;
            }
            else
            {
                marksmanship = attacking.Character.CharacterStats.GetMarksmanship();
            }
            bullet.rigidbody.AddForce(new Vector3((float)(((random.NextDouble() * 2) - 1) * spreadNormalized * Math.Pow(0.9, marksmanship)),
                                                  (float)(((random.NextDouble() * 2) - 1) * spreadNormalized * Math.Pow(0.9, marksmanship)),
                                                  (float)(((random.NextDouble() * 2) - 1) * spreadNormalized * Math.Pow(0.9, marksmanship))), ForceMode.VelocityChange);
        }

    }

    public enum EAttackStatus
    {
        NoAmmo,
        Aiming,
        OutOfRange,
        Attacking
    }
    
}
