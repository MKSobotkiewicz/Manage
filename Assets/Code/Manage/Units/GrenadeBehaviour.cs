using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    class GrenadeBehaviour : MonoBehaviour
    {
        public GameObject Explosion;
        public int Damage;
        public Player.Player Player;
        public Unit Unit;

        private float timer=4f;

        private List<Collider> triggerList=new List<Collider>();

        private static System.Random random = new System.Random();

        public void Start()
        {
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)

            {
                Destroy();
            }
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (triggerList.Contains(collider))
            {
                return;
            }
            if (collider.GetComponentInParent<Unit>()==null)
            {
                return;
            }
            triggerList.Add(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            if (triggerList.Contains(collider))
            {
                triggerList.Remove(collider);
            }
        }

        public void Destroy()
        {
            var damagedUnitList = new List<Unit>();
            foreach (var trigger in triggerList)
            {
                if ( trigger == null)
                {
                    continue;
                }
                var unit = trigger.GetComponentInParent<Unit>();
                if (unit != null)
                {
                    if (damagedUnitList.Contains(unit))
                    {
                        continue;
                    }
                    var damage = random.Next((int)(Damage * 0.5), (int)(Damage * 1.5));
                    unit.Damage(damage, Player,Unit);
                    damagedUnitList.Add(unit);
                    if (unit.Inventory.VehicleType == null)
                    {
                        var bh = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHitFlesh") as GameObject);
                        bh.transform.position = trigger.transform.position;
                    }
                }
            }
            var go = Instantiate(Explosion, transform);
            go.transform.position += new Vector3(0,1,0);
            go.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
