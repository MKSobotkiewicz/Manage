﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Manage.Space;
using Manage.Characters;

namespace Manage.Units
{
    public class Unit : MonoBehaviour, IPositioned, IDestructable
    {
        public Inventory Inventory { get; set; }
        public Character Character { get; set; }
        public bool Selected { get; set; } = false;
        public bool Dead { get; private set; } = false;
        public bool Attacking { get; private set; } = false;
        public bool Reloading { get; private set; } = false;
        public bool ThrowingGrenade { get; private set; } = false;
        public bool Shot { get; private set; } = false;
        public Player.Player Player { get; set; }
        public List<Animator> Animators = new List<Animator>();
        public Dictionary<Unit, float> AttackedBy;
        public Dialog.DialogManager DialogManager;
        public UI.FloatingLifePanel FloatingLifePanel;

        private int hitPoints;

        private float shotTime = 0;
        private float grenadeTime = 0;
        private float randomTime = 0;
        private float ThrowingGrenadeTime = 0;
        private float HealTime = 0;
        private NavMeshAgent navMeshAgent;
        private Audio.UnitsScreamsManager unitsScreamsManager;
        private Audio.Footsteps footsteps;

        private Unit target;
        private Unit grenadeTarget;

        private static readonly string bloodPath = "Units/GroundBlood";

        private static readonly System.Random random = new System.Random();

        public Unit()
        {
        }

        private Unit CopyArmor(ArmorType armorType)
        {
            var factory = new UnitFactory();
            WeaponType weaponType = null;
            if (Inventory.Weapon != null)
            {
                weaponType = Inventory.Weapon.WeaponType;
            }
            HelmetType helmetType = null;
            if (Inventory.Helmet != null)
            {
                helmetType = Inventory.Helmet.HelmetType;
            }
            VestType vestType = null;
            if (Inventory.Vest != null)
            {
                vestType = Inventory.Vest.VestType;
            }
            var grenadeType = Inventory.GrenadeType;
            var unit = factory.Create(null,
                                      weaponType,
                                      armorType,
                                      helmetType,
                                      vestType,
                                      grenadeType,
                                      Inventory.VehicleType,
                                      Character,
                                      transform);

            unit.Inventory.Weapon.Ammo = Inventory.Weapon.Ammo;

            unit.Selected = Selected;
            unit.Dead = Dead;
            unit.Attacking = Attacking;
            unit.Reloading = Reloading;
            unit.ThrowingGrenade = ThrowingGrenade;
            unit.Shot = Shot;
            unit.Player = Player;
            unit.hitPoints = hitPoints;

            unit.shotTime = shotTime;
            unit.grenadeTime = grenadeTime;
            unit.randomTime = randomTime;
            unit.ThrowingGrenadeTime = ThrowingGrenadeTime;
            unit.HealTime = HealTime;

            unit.target = target;

            if (Character.Organization == Player.Organization)
            {
                Player.Units.Add(unit);
            }

            return unit;
        }

        public Unit ChangeArmor(Unit unitToRearm, ArmorType armorType)
        {
            var unit = unitToRearm.CopyArmor(armorType);
            unitToRearm.Annihilate();
            return unit;
        }

        public Unit Copy()
        {
            return CopyArmor(Inventory.ArmorType);
        }

        public void Start()
        {
            UnitShaderController.SetColor(this,Player);
            unitsScreamsManager = GetComponentInChildren<Audio.UnitsScreamsManager>();
            footsteps = GetComponentInChildren<Audio.Footsteps>();
            AttackedBy = new Dictionary<Unit, float>();
            hitPoints = GetMaxHitPoints();
            grenadeTime = (float)random.NextDouble() * 25 + 5 - 10 * Character.CharacterTraits.Contains(CharacterTraitsList.Grenadier);
            randomTime = (float)random.NextDouble() * 30 + 5;
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            }
            if (GetComponent<Animator>() == null)
            {
                gameObject.AddComponent<Animator>();
            }
            if (Inventory.VehicleType == null)
            {
                Animators.AddRange(GetComponents<Animator>());
                var locRandom = (float)random.NextDouble() * 5;
                SetAnimatorsFloat("Random", locRandom);
            }

            SetSpeed();

            SetAnimatorsBool("Pistol", Inventory.Weapon.WeaponType.IsPistol);
            var transform = GetComponent<Transform>().position;
        }

        public void Update()
        {
            if (!Dead)
            {
                if (randomTime >= 0)
                {
                    randomTime -= Time.deltaTime;
                }
                else
                {
                    randomTime = (float)random.NextDouble() * 30 + 5;
                    var locRandom = (float)random.NextDouble() * 5;
                    SetAnimatorsFloat("Random", locRandom);
                }
                if (HealTime >= 0)
                {
                    HealTime -= Time.deltaTime;
                }
                else
                {
                    HealTime = 1;
                    if (Inventory.VehicleType == null)
                    {
                        Heal(1 + Character.CharacterTraits.Contains(CharacterTraitsList.Healthy));
                    }
                }
                SetAnimatorsFloat("Speed", navMeshAgent.velocity.magnitude);
                if (IsMoving())
                {
                    Attacking = false;
                    return;
                }
                if (ThrowingGrenade)
                {
                    transform.LookAt(grenadeTarget.transform);
                    foreach (var animator in Animators)
                    {
                        if (animator != null)
                        {
                            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Toss Grenade"))
                            {
                                animator.SetBool("Shooting", false);
                            }
                        }
                    }
                    ThrowingGrenadeTime -= Time.deltaTime;
                    if (ThrowingGrenadeTime <= 0)
                    {
                        EndTossGrenade();
                    }
                    return;
                }
                SetAnimatorsBool("Shooting", Attacking);
                if (Attacking)
                {
                    Attack(target);
                    if (Inventory.VehicleType == null)
                    {
                        grenadeTime -= Time.deltaTime;
                    }
                    if (grenadeTime <= 0)
                    {
                        grenadeTime = (float)random.NextDouble() * 25 + 5 - 10 * Character.CharacterTraits.Contains(CharacterTraitsList.Grenadier);
                        StartTossGrenade();
                    }
                }
                shotTime -= Time.deltaTime;
                if (shotTime <= 0)
                {
                    shotTime = 0;
                    EndShot();
                }
            }
        }

        public int GetMaxHitPoints()
        {
            if (Inventory == null)
            {
                throw new Exception(gameObject.name);
            }
            if (Inventory.VehicleType == null)
            {
                return GetHitPoints(Endurance());
            }
            return (int)Inventory.VehicleType.HitPoints;
        }

        public static int GetHitPoints(uint endurance)
        {
            return (int)(100 * (1+Mathf.Log10(endurance)));
        }

        public static float GetSpeed(uint stamina)
        {
            return 4 * (1+Mathf.Log10(stamina));
        }

        public static double GetWeaponSpread(uint marksmanship)
        {
            return Math.Pow(0.92, marksmanship-1);
        }

        public static float AbilitiesLoadTime(Player.Player player)
        {
            uint command = 0;
            foreach (var _unit in player.Units)
            {
                command += _unit.Command();
            }
            return (float)Math.Pow(0.98, command - 1);
        }

        public static float AbilitiesLoadTimeSingle(uint command)
        {
            return (float)Math.Pow(0.98, command - 1);
        }

        public bool Arm(WeaponType weaponType)
        {
            SetAnimatorsBool("Pistol", weaponType.IsPistol);
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == "WeaponPoint")
                {
                    Inventory.Arm(weaponType, child);
                    return true;
                }
            }
            UnityEngine.Debug.Log("No WeaponPoint on Unit.");
            return false;
        }

        public bool PutOnHelmet(HelmetType helmetType)
        {
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == "HelmetPoint")
                {
                    Inventory.PutOnHelmet(helmetType, child);
                    return true;
                }
            }
            UnityEngine.Debug.Log("No HelmetPoint on Unit.");
            return false;
        }

        public bool PutOnVest(VestType vestType)
        {
            Inventory.PutOnVest(vestType, transform, Character.Gender);
            if (Inventory.Vest != null)
            {
                Animators.AddRange(Inventory.Vest.GetComponents<Animator>());
                var locRandom = (float)random.NextDouble() * 5;
                foreach (var animator in Animators)
                {
                    if (animator != null)
                    {
                        animator.SetFloat("Random", locRandom);
                        gameObject.SetActive(false);
                        gameObject.SetActive(true);
                    }
                }
            }
            if (Inventory.Weapon != null)
            {
                SetAnimatorsBool("Pistol", Inventory.Weapon.WeaponType.IsPistol);
            }
            return true;
        }

        public bool ChangeGrenade(GrenadeType grenadeType)
        {
            return Inventory.ArmGrenade(grenadeType);
        }

        public WeaponType Rearm(WeaponType weaponType)
        {
            if (weaponType != null)
            {
                SetAnimatorsBool("Pistol", weaponType.IsPistol);
            }
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == "WeaponPoint")
                {
                    return Inventory.Rearm(weaponType, child);
                }
            }
            UnityEngine.Debug.Log("No WeaponPoint on Unit.");
            return null;
        }

        public void Attack(Unit _target)
        {
            if (_target == null)
            {
                Attacking = false;
                return;
            }
            if (_target.Dead)
            {
                Attacking = false;
                return;
            }
            if (Shot || IsMoving())
            {
                return;
            }
            this.target = _target;
            Attacking = true;
            if (!ThrowingGrenade)
            {
                if (Inventory.VehicleType == null)
                {
                    transform.LookAt(target.transform);
                }
                else
                {
                    GetComponent<VehicleBehaviour>().Target = target.gameObject;
                }
            }
            if (Inventory.Weapon != null)
            {
                if (!Inventory.Weapon.Attack(this, target))
                {
                    target.AttackedBy.Remove(this);
                    Attacking = false;
                    MoveTo(Vector3.Lerp(target.Position(), Position(), 0.8f));
                    return;
                }
                target.AttackedBy[this] = Vector3.Distance(Position(), target.Position());
            }
        }

        public void Select()
        {
            Selected = true;
            UnitShaderController.SetSelected(this);
        }

        public void Unselect()
        {
            Selected = false;
            UnitShaderController.SetUnselected(this);
        }

        public void AttackWithGrenade(Unit _target)
        {
            if (_target == null || _target.Dead || Shot)
            {
                return;
            }
            grenadeTarget = _target;
        }

        private void SetAnimatorsBool(string parameter, bool value)
        {
            foreach (var animator in Animators)
            {
                if (animator != null)
                {
                    animator.SetBool(parameter, value);
                }
            }
        }

        private void SetAnimatorsFloat(string parameter, float value)
        {
            foreach (var animator in Animators)
            {
                if (animator != null)
                {
                    animator.SetFloat(parameter, value);
                }
            }
        }

        private bool MoveTo(Vector3 position)
        {
            if (Dead /*|| Shot*/)
            {
                return false;
            }
            if (Reloading)
            {
                EndReload();
            }
            if (ThrowingGrenade)
            {
                SetAnimatorsBool("Toss Grenade", false);
                ThrowingGrenade = false;
                ThrowingGrenadeTime = 2;
            }
            Attacking = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(position);
            return true;
        }

        public void Stop()
        {
            navMeshAgent.velocity = new Vector3(0, 0, 0);
            navMeshAgent.isStopped = true;
        }

        public void StartReload()
        {
            Stop();
            Reloading = true;
            SetAnimatorsBool("Reloading", Reloading);
        }

        public void EndReload()
        {
            Reloading = false;
            SetAnimatorsBool("Reloading", Reloading);
        }

        private void StartShot()
        {
            Shot = true;
            SetAnimatorsBool("Crouching", Shot);
        }

        private void EndShot()
        {
            Shot = false;
            SetAnimatorsBool("Crouching", Shot);
        }

        private void StartTossGrenade()
        {
            if (grenadeTarget == null)
            {
                grenadeTarget = target;
                if (grenadeTarget.Dead)
                {
                    grenadeTarget = null;
                }
            }
            else if (grenadeTarget.Dead)
            {
                grenadeTarget = target;
                if (grenadeTarget.Dead)
                {
                    grenadeTarget = null;
                }
            }
            if (grenadeTarget == null)
            {
                return;
            }
            var distance = Vector3.Distance(transform.position, grenadeTarget.transform.position);
            if (Inventory.GrenadeType != null)
            {
                if (distance > 30 && distance < Inventory.GrenadeType.Range)
                {
                    SetAnimatorsBool("Shooting", false);
                    SetAnimatorsBool("Toss Grenade", true);
                    ThrowingGrenade = true;
                    Attacking = false;
                    ThrowingGrenadeTime = 2;
                    return;
                }
            }
        }

        private void EndTossGrenade()
        {
            Transform weaponPoint = transform;
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == "WeaponPoint")
                {
                    weaponPoint = child;
                }
            }
            var grenade = Inventory.GrenadeType.Instantiate(transform.parent, weaponPoint.position, transform.rotation);
            var distance = Vector3.Distance(transform.position, grenadeTarget.transform.position);
            grenade.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,
                                                                           distance / 10 + 10,
                                                                           distance / 4 + 1), ForceMode.VelocityChange);
            grenade.GetComponent<Rigidbody>().AddTorque(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
            grenade.GetComponent<GrenadeBehaviour>().Player = Player;
            ThrowingGrenade = false;
            SetAnimatorsBool("Toss Grenade", false);
        }

        public bool IsMoving()
        {
            var speed = navMeshAgent.velocity.magnitude;
            if (footsteps != null)
            {
                footsteps.SetSpeed(speed);
            }
            if (speed > 0.1)
            {
                return true;
            }
            return false;
        }

        public bool IsInRange(Unit unit)
        {
            if (Inventory.Weapon.WeaponType.IsInRange(Vector3.Distance(Position(), unit.Position()), Character))
            {
                return true;
            }
            return false;
        }

        public Vector3 Position()
        {
            return transform.position;
        }

        public IPositioned Move(Vector3 vector)
        {
            MoveTo(vector);
            return this;
        }

        public IPositioned Rotate(Vector3 eulerAngles)
        {
            GetComponent<Transform>().Rotate(eulerAngles);
            return this;
        }

        public int Armor()
        {
            return Inventory.GetArmor();
        }

        public int HitPoints()
        {
            return hitPoints;
        }

        public uint Endurance()
        {
            return Character.CharacterStats.GetEndurance() + Inventory.GetEndurance();
        }

        public uint Marksmanship()
        {
            return Character.CharacterStats.GetMarksmanship() + Inventory.GetMarksmanship();
        }

        public uint Command()
        {
            return Character.CharacterStats.GetCommand() + Inventory.GetCommand();
        }

        public uint Charisma()
        {
            return Character.CharacterStats.GetCharisma() + Inventory.GetCharisma();
        }

        public uint Stamina()
        {
            return Character.CharacterStats.GetStamina() + Inventory.GetStamina();
        }

        public bool Damage(int value)
        {
            return Damage(value,null);
        }

        public bool Damage(int value,Player.Player damagingPlayer)
        {
            if (Inventory.VehicleType == null)
            {
                Attacking = false;
                StartShot();
                shotTime = (float)((random.NextDouble() + 0.5) * (3 - Character.CharacterTraits.Contains(CharacterTraitsList.Unyielding) * 2));
                hitPoints -= Mathf.Max(value - Inventory.GetArmor() - Character.CharacterTraits.Contains(CharacterTraitsList.Tough) * 5, 0);
                UnitShaderController.SetBlood(this, 1f-((float)hitPoints / (float)GetMaxHitPoints()));
            }
            else
            {
                hitPoints -= Mathf.Max(value - Inventory.GetArmor(), 0);
            }
            if (!(FloatingLifePanel is null))
            {
                FloatingLifePanel.SetHitPoints();
            }
            if (hitPoints <= 0)
            {
                if (damagingPlayer != null)
                {
                    damagingPlayer.ExperienceManager.AddExperienceToUnits(Character.ExperienceFromCharacter());
                }
                Kill();
                return true;
            }
            return false;
        }

        public void Kill()
        {
            Unselect();
            Inventory.Drop(transform);
            var blood=Instantiate(UnityEngine.Resources.Load(bloodPath) as GameObject,gameObject.transform);
            blood.transform.localPosition += new Vector3(0,0.05f,0);
            if (unitsScreamsManager != null && Inventory.VehicleType == null)
            {
                unitsScreamsManager.Play();
            }
            Dispose();
        }

        public void Dispose()
        {
            EndShot();
            var locRandom = (float)random.NextDouble() * 5;
            foreach (var animator in Animators)
            {
                if (animator != null)
                {
                    animator.SetFloat("Random", locRandom);
                    animator.SetBool("Dead", true);
                }
            }
            Dead = true;
            navMeshAgent.velocity = new Vector3(0, 0, 0);
            navMeshAgent.isStopped = true;
            var rigidbody = this.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            Destroy(this.GetComponent<CapsuleCollider>());
            if (Player != null)
            {
                Player.Units.Remove(this);
            }
            AllUnitsList.Units.Remove(this);
        }

        public void Annihilate()
        {
            Dispose();
            Destroy(gameObject);
        }

        public bool CanBePenetrated(BulletType bulletType)
        {
            if (Inventory.VehicleType == null)
            {
                return (bulletType.Damage > Inventory.GetArmor() + Character.CharacterTraits.Contains(CharacterTraitsList.Tough) * 5);
            }
            return (bulletType.Damage > Inventory.GetArmor());
        }

        public bool CanPenetrate(Unit unit)
        {
            if (Inventory.VehicleType == null)
            {
                return (unit.CanBePenetrated(Inventory.Weapon.WeaponType.BulletType));
            }
            return (unit.CanBePenetrated(Inventory.VehicleType.WeaponType.BulletType));
        }

        private void SetSpeed()
        {
            if (Inventory.VehicleType == null)
            {
                navMeshAgent.speed = GetSpeed(Stamina());
                return;
            }
            navMeshAgent.speed = Inventory.VehicleType.Speed;
        }

        public void Heal(int value)
        {
            hitPoints += value;
            UnitShaderController.SetBlood(this, 1f - ((float)hitPoints / (float)GetMaxHitPoints()));
            if (hitPoints >= GetMaxHitPoints())
            {
                hitPoints = GetMaxHitPoints();
            }
            if (!(FloatingLifePanel is null))
            {
                FloatingLifePanel.SetHitPoints();
            }
        }
    }
}