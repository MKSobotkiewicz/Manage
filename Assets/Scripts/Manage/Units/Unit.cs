using System;
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
        private static System.Random random = new System.Random();

        public Inventory Inventory { get; set; }
        public Character Character { get; set; }
        public bool Selected { get; set; } = false;
        public bool Dead { get; private set; } = false;
        public bool Attacking { get; private set; } = false;
        public bool Reloading { get; private set; } = false;
        public bool ThrowingGrenade { get; private set; } = false;
        public bool Shot { get; private set; } = false;
        public bool Player { get; set; } = false;

        private int hitPoints;

        private float shotTime=0;
        private float grenadeTime = 0;
        private float randomTime = 0;
        private float ThrowingGrenadeTime = 0;
        private float HealTime = 0;
        private NavMeshAgent navMeshAgent;
        public List<Animator> animators = new List<Animator>();

        private Unit target;

        public Unit()
        {
        }

        /*public static Unit Create(Player.Player player,
                                  WeaponType weaponType,
                                  ArmorType armorType,
                                  HelmetType helmetType,
                                  VestType vestType,
                                  Vehicle vehicle,
                                  Character character,
                                  Transform transform)
        {
            GameObject go;
            if (vehicle==null)
            {
                go = Inventory.PutOnArmor(armorType, transform, character.Gender);
            }
            else
            {
                go = Inventory.SpawnVehicle(vehicle, transform);
            }
            go.transform.parent = null;
            var unit = go.GetComponent<Unit>();
            unit.Character = character;
            unit.animators = new List<Animator>();
            unit.Selected = false;
            unit.Dead = false;
            unit.Attacking = false;
            unit.Reloading = false;
            unit.ThrowingGrenade = false;
            unit.hitPoints = unit.GetMaxHitPoints();
            unit.randomTime = (float)random.NextDouble() * 30 + 5;
            var children = unit.GetComponentsInChildren<Transform>();
            unit.grenadeTime = (float)random.NextDouble() * 25 + 5 - 10 * unit.Character.CharacterTraits.Contains(CharacterTraitsList.Grenadier);
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
        }*/

        private Unit CopyArmor(ArmorType armorType)
        {
            var factory = new UnitFactory();
            var unit = factory.Create(null,
                                      Inventory.Weapon.WeaponType,
                                      armorType,
                                      Inventory.Helmet.HelmetType,
                                      Inventory.Vest.VestType,
                                      Inventory.Vehicle,
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

            return unit;
        }

        public Unit ChangeArmor(Unit unitToRearm, ArmorType armorType)
        {
            var unit= unitToRearm.CopyArmor(armorType);
            unitToRearm.Annihilate();
            return unit;
        }

        public Unit Copy()
        {
            return CopyArmor(Inventory.ArmorType);
        }

        public void Start()
        {
            hitPoints = GetMaxHitPoints();
            grenadeTime = (float)random.NextDouble() * 25 + 5 - 10 * Character.CharacterTraits.Contains(CharacterTraitsList.Grenadier);
            randomTime = (float)random.NextDouble() * 30 + 5;
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                navMeshAgent=gameObject.AddComponent<NavMeshAgent>();
            }
            if (GetComponent<Animator>() == null)
            {
                gameObject.AddComponent<Animator>();
            }
            if (Inventory.Vehicle == null)
            {
                animators.AddRange(GetComponents<Animator>());
                var locRandom = (float)random.NextDouble() * 5;
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetFloat("Random", locRandom);
                    }
                }
            }

            SetSpeed();

            var transform = GetComponent<Transform>().position;
        }

        public void Update()
        {
            if (!Dead)
            {
                if (randomTime >= 0)
                {
                    randomTime -= Time.fixedDeltaTime;
                }
                else
                {
                    randomTime = (float)random.NextDouble() * 30 + 5;
                    var locRandom = (float)random.NextDouble() * 5;
                    foreach (var animator in animators)
                    {
                        if (animator != null)
                        {
                            animator.SetFloat("Random", locRandom);
                        }
                    }
                }
                if (HealTime >= 0)
                {
                    HealTime -= Time.fixedDeltaTime;
                }
                else
                {
                    HealTime = 1;
                    Heal(1+Character.CharacterTraits.Contains(CharacterTraitsList.Healthy));
                }
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
                    }
                }
                if (IsMoving())
                {
                    Attacking = false;
                    return;
                }
                if (ThrowingGrenade)
                {
                    foreach (var animator in animators)
                    {
                        if (animator != null)
                        {
                            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Toss Grenade"))
                            {
                                animator.SetBool("Shooting", false);
                                animator.SetBool("Toss Grenade", false);
                            }
                        }
                    }
                    ThrowingGrenadeTime -= Time.fixedDeltaTime;
                    if (ThrowingGrenadeTime <= 0)
                    {
                        Transform weaponPoint = transform;
                        foreach (var child in GetComponentsInChildren<Transform>())
                        {
                            if (child.name == "WeaponPoint")
                            {
                                weaponPoint = child;
                            }
                        }
                        var grenade = Inventory.Grenade.Instantiate(transform.parent, weaponPoint.position, transform.rotation);
                        var distance = Vector3.Distance(transform.position, target.transform.position);
                        grenade.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,
                                                                                       distance / 10+10,
                                                                                       distance / 4+1), ForceMode.VelocityChange);
                        grenade.GetComponent<Rigidbody>().AddTorque(new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()));
                        ThrowingGrenade = false;
                        foreach (var animator in animators)
                        {
                            if (animator != null)
                            {
                                animator.SetBool("Toss Grenade", false);
                            }
                        }
                    }
                    return;
                }
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("Shooting", Attacking);
                    }
                }
                if (Attacking)
                {
                    Attack(target);
                    grenadeTime -= Time.fixedDeltaTime;
                    if (grenadeTime <= 0)
                    {
                        grenadeTime = (float)random.NextDouble() * 25+5-10* Character.CharacterTraits.Contains(CharacterTraitsList.Grenadier);
                        TossGrenade();
                    }
                }
                shotTime -= Time.fixedDeltaTime;
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
            if (Inventory.Vehicle==null)
            {
                return (int)(100 * (1 + Mathf.Log(Character.CharacterStats.GetEndurance()+Inventory.GetEndurance())));
            }
            return (int)Inventory.Vehicle.HitPoints;
        }

        public void Arm(Weapon weapon)
        {
            Inventory.Arm(weapon);
        }

        public bool Arm(WeaponType weaponType)
        {
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == "WeaponPoint")
                {
                    Inventory.Arm(weaponType,child);
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
            animators.AddRange(Inventory.Vest.GetComponents<Animator>());
            var locRandom = (float)random.NextDouble() * 5;
            foreach (var animator in animators)
            {
                if (animator != null)
                {
                    animator.SetFloat("Random", locRandom);
                    gameObject.SetActive(false);
                    gameObject.SetActive(true);
                }
            }
            return true;
        }

        public WeaponType Rearm(WeaponType weaponType)
        {
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

        public void Attack(Unit target)
        {
            if (target == null)
            {
                Attacking = false;
                return;
            }
            if (target.Dead)
            {
                Attacking = false;
                return;
            }
            if (Shot)
            {
                return;
            }
            this.target = target;
            Attacking = true;
            if (Inventory.Vehicle == null)
            {
                transform.LookAt(target.transform);
            }
            else
            {
                GetComponent<VehicleBehaviour>().Target = target.gameObject;
            }
            if (Inventory.Weapon != null)
            {
                if (!Inventory.Weapon.Attack(this, target))
                {
                    Attacking = false;
                    MoveTo(Vector3.Lerp(target.Position(),Position(),0.8f));
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
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("Toss Grenade", false);
                    }
                }
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

        public void Select()
        {

        }

        public void StartReload()
        {
            Stop();
            Reloading = true;
            foreach (var animator in animators)
            {
                if (animator != null)
                {
                    animator.SetBool("Reloading", Reloading);
                }
            }
        }

        public void EndReload()
        {
            Reloading = false;
            foreach (var animator in animators)
            {
                if (animator != null)
                {
                    animator.SetBool("Reloading", Reloading);
                }
            }
        }

        public void StartShot()
        {
            Shot = true;
            foreach (var animator in animators)
            {
                if (animator != null)
                {
                    animator.SetBool("Crouching", Shot);
                }
            }
        }

        public void EndShot()
        {
            Shot = false;
            foreach (var animator in animators)
            {
                if (animator != null)
                {
                    animator.SetBool("Crouching", Shot);
                }
            }
        }

        public void TossGrenade()
        {
            if(Vector3.Distance(transform.position, target.transform.position)> 30)
            {
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("Shooting", false);
                    }
                }
                ThrowingGrenade = true;
                Attacking = false;
                ThrowingGrenadeTime = 2;
                foreach (var animator in animators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("Toss Grenade", true);
                    }
                }
            }
        }

        public bool IsMoving()
        {
            if (navMeshAgent.velocity.magnitude > 0.1)
            {
                return true;
            }
            return false;
        }

        public bool IsInRange(Unit unit)
        {
            if (Inventory.Weapon.WeaponType.IsInRange(Vector3.Distance(Position(), unit.Position()),Character))
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

        public bool Damage(int value)
        {
            if (Inventory.Vehicle == null)
            {
                Attacking = false;
                StartShot();
                shotTime = (float)((random.NextDouble() + 0.5) * (3- Character.CharacterTraits.Contains(CharacterTraitsList.Unyielding) * 2));
                hitPoints -= Mathf.Max(value-Inventory.GetArmor() - Character.CharacterTraits.Contains(CharacterTraitsList.Tough)*5,0);
            }
            else
            {
                hitPoints -= Mathf.Max(value- Inventory.Vehicle.Armour,0);
            }
            if (hitPoints <= 0)
            {
                Dispose();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            EndShot();
            var locRandom = (float)random.NextDouble() * 5;
            foreach (var animator in animators)
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
            AllUnitsList.Units.Remove(this);
        }

        public void Annihilate()
        {
            Dispose();
            Destroy(gameObject);
        }

        private void SetSpeed()
        {
            if (Inventory.Vehicle==null)
            {
                navMeshAgent.speed = (16 * (1 + Mathf.Log(Character.CharacterStats.GetStamina()+Inventory.GetStamina()))) / 4;
                return;
            }
            navMeshAgent.speed = Inventory.Vehicle.Speed;
        }

        private void Heal(int value)
        {
            hitPoints += value;
            if (hitPoints >= GetMaxHitPoints())
            {
                hitPoints = GetMaxHitPoints();
            }
        }
    }
}
