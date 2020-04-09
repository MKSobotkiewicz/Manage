using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public class Bullet : MonoBehaviour
    {
        public BulletType BulletType { get; protected set; }
        public new Rigidbody rigidbody;

        protected Vector3 lastPosition;
        protected Organizations.Organization organization;
        public Player.Player Player;
        protected bool dying = false;
        protected float lifetime;

        private static System.Random random = new System.Random();

        public static Bullet Create(BulletType bulletType, Unit unit, Vector3 position, Vector3 target)
        {
            var go = Instantiate(UnityEngine.Resources.Load(bulletType.PrefabPath) as GameObject);
            if (Equals(go, null))
            {
                UnityEngine.Debug.Log("Failed to load bullet prefab " + bulletType.PrefabPath);
            }
            go.transform.parent = null;
            go.transform.position = position;
            go.transform.localScale=new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.LookAt(target + new Vector3(0, 1.5f, 0), new Vector3(0, 1, 0));
            go.transform.Rotate(new Vector3(0, 1, 0), 90);
            go.transform.Rotate(new Vector3(1, 0, 0), 90);
            var bullet = go.GetComponent<Bullet>();
            bullet.BulletType = bulletType;

            bullet.rigidbody = go.GetComponent<Rigidbody>();
            bullet.rigidbody.mass = (float)bulletType.Mass;
            bullet.rigidbody.AddRelativeForce(new Vector3(-(float)bulletType.Velocity, 0, 0), ForceMode.VelocityChange);
            bullet.lifetime = 2;
            if (unit != null)
            {
                bullet.organization = unit.Character.Organization;
                bullet.Player = unit.Player;
            }
            
            return bullet;
        }

        private void Update()
        {
            lastPosition = transform.position;
            if (dying)
            {
                lifetime -= Time.fixedDeltaTime;
                if (lifetime <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var unit = collision.transform.GetComponentInParent<Unit>();
            if (unit != null)
            {
                if (Equals(unit.Character.Organization, organization))
                {
                    return;
                }
                dying = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                unit.Damage(BulletType.Damage, Player);
            }
            if (this is ExplosiveShell)
            {
                UnityEngine.Debug.Log("Brain aunerysm");
                ((ExplosiveShell)this).Hit(collision);
            }
            else
            {
                Hit(collision);
            }
            if (random.NextDouble() > 0.5)
            {
                dying = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                return;
            }
            rigidbody.velocity /= 2;
        }

        protected void Hit(Collision collision)
        {
            if (dying is false)
            {
                switch (collision.gameObject.tag)
                {
                    case "Metal":
                        InstantiateBulletHit("Bullets/BulletHitMetal", collision);
                        break;
                    case "Flesh":
                        InstantiateBulletHit("Bullets/BulletHitFlesh", collision);
                        break;
                    case "Concrete":
                        InstantiateBulletHit("Bullets/BulletHitConcrete", collision);
                        break;
                    default:
                        InstantiateBulletHit("Bullets/BulletHit", collision);
                        break;
                }
            }
            rigidbody.velocity /= 2;
            dying = true;
        }

        private void InstantiateBulletHit(string name, Collision collision)
        {
            var go = Instantiate(UnityEngine.Resources.Load(name) as GameObject);
            go.transform.position = collision.GetContact(0).point;
            go.transform.rotation = Quaternion.LookRotation(collision.GetContact(0).normal);
        }

        public void Rotate(Vector3 rotation)
        {
            transform.Rotate(rotation);
        }
    }
}
