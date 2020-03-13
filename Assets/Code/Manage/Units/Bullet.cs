using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Units
{
    public class Bullet : MonoBehaviour
    {
        public BulletType BulletType { get; private set; }
        public new Rigidbody rigidbody;

        private Vector3 lastPosition;
        private Organizations.Organization organization;
        private bool dying = false;
        private float lifetime;

        private static System.Random random = new System.Random();

        public Bullet(BulletType bulletType, Vector3 position, Quaternion rotation)
        {
            BulletType = bulletType;
            rigidbody = gameObject.AddComponent<Rigidbody>();

            transform.position = position;
            transform.rotation = rotation;

            rigidbody.mass = (float)BulletType.Mass;
            rigidbody.AddForce(rotation.eulerAngles.normalized * (float)BulletType.Velocity);
        }

        public static Bullet Create(BulletType bulletType, Unit unit, Transform transform, Vector3 target)
        {
            var go = Instantiate(UnityEngine.Resources.Load(bulletType.PrefabPath) as GameObject, transform,false);
            if (Equals(go, null))
            {
                UnityEngine.Debug.Log("Failed to load bullet prefab " + bulletType.PrefabPath);
            }
            go.transform.parent = null;
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
            bullet.organization = unit.Character.Organization;
            
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

        /*private void OnTriggerEnter(Collider collider)
        {
            UnityEngine.Debug.Log("HIT");
            var unit = collider.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                UnityEngine.Debug.Log(unit.Character.Organization.Name);
                UnityEngine.Debug.Log(organization.Name);
                if (Equals(unit.Character.Organization, organization))
                {
                    return;
                }
                unit.Damage(BulletType.Damage);
                var go = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHitBlood") as GameObject);
                go.transform.position = Vector3.Lerp(lastPosition,transform.position,0.5f);
            }
        }*/

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
                unit.Damage(BulletType.Damage);
                if (unit.Inventory.VehicleType == null)
                {
                    var go = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHitBlood") as GameObject);
                    go.transform.position = collision.GetContact(0).point;
                }
                else
                {
                    var go = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHit") as GameObject);
                    go.transform.position = collision.GetContact(0).point;
                }
            }
            else
            {
                var go = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHit") as GameObject);
                go.transform.position = collision.GetContact(0).point;
            }
            if (random.NextDouble() > 0.5)
            {
                dying = true;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                rigidbody.velocity /= 2;
            }
        }

        public void Rotate(Vector3 rotation)
        {
            transform.Rotate(rotation);
        }
    }
}
