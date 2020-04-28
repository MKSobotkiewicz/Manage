using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public class Weapon: MonoBehaviour
    {
        public WeaponType WeaponType { get; private set; }

        public int Ammo { get; set; }
        public double TimeSinceLastShot { get; private set; }
        public double TimeSinceReloadStart { get; private set; }

        private bool isReloading;
        private float aimingTime;

        private new Transform transform;
        private Unit unit;
        private Animator animator;

        private System.Random random = new System.Random();

        public Weapon(WeaponType weaponType)
        {
            WeaponType = weaponType;
            Ammo = (int)WeaponType.Ammo;
            isReloading = false;
            aimingTime = (float)((random.NextDouble()+0.5)* WeaponType.AimingTime);
        }

        public static Weapon Create(WeaponType weaponType,Transform transform)
        {
            var wgo = Instantiate((UnityEngine.Resources.Load(weaponType.PrefabPath) as GameObject), transform);
            var weapon= wgo.GetComponent<Weapon>();
            weapon.WeaponType = weaponType;
            weapon.Ammo = (int)weaponType.Ammo;
            weapon.isReloading = false;
            weapon.random = new System.Random();
            weapon.aimingTime = (float)((weapon.random.NextDouble() + 0.5) * weapon.WeaponType.AimingTime);
            return weapon; 
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Start()
        {
            transform = GetComponent<Transform>();
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            TimeSinceLastShot += Time.deltaTime;
            if (isReloading)
            {
                TimeSinceReloadStart += Time.deltaTime;
                if (TimeSinceReloadStart > WeaponType.ReloadTime)
                {
                    ReloadComplete();
                }
            }
        }

        public bool Attack(Unit attacking, Unit target)
        {
            if (isReloading)
            {
                return true;
            }
            switch (WeaponType.Attack(attacking, target,this, TimeSinceLastShot,aimingTime,Ammo))
            {
                case EAttackStatus.Aiming:
                    return true;
                case EAttackStatus.OutOfRange:
                    return false;
                case EAttackStatus.NoAmmo:
                    ReloadStart(attacking);
                    return true;
                case EAttackStatus.Attacking:
                    Fire();
                    return true;
                default:
                    return true;
            }
        }

        public void ReloadStart(Unit reloading)
        {
            isReloading = true;
            unit = reloading;
            unit.StartReload();
        }

        public void ReloadComplete()
        {
            isReloading = false;
            Ammo = (int)WeaponType.Ammo;
            unit.EndReload();
            TimeSinceReloadStart = 0;
        }

        private void Fire()
        {
            if (animator != null)
            {
                animator.SetTrigger("Fire");
            }
            TimeSinceLastShot = 0;
            aimingTime = (float)((random.NextDouble() + 0.5) * WeaponType.AimingTime);
            Ammo -= 1;
        }
    }
}
