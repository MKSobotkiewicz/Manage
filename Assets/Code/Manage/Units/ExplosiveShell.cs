using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class ExplosiveShell:Bullet
    {
        public new void Hit(Collision collision)
        {
            if (dying is true)
            {
                return;
            }
            var damagedUnitList = new List<Unit>();
            foreach (var unit in AllUnitsList.Units)
            {
                if (unit is null)
                {
                    continue;
                }
                if (Vector3.Distance(transform.position, unit.Position()) > ((ExplosiveShellType)BulletType).Radius)
                {
                    continue;
                }
                damagedUnitList.Add(unit);
                if (unit.Inventory.VehicleType is null)
                {
                    var bh = Instantiate(UnityEngine.Resources.Load("Bullets/BulletHitFlesh") as GameObject);
                    bh.transform.position = unit.transform.position;
                }
            }
            for (int i = 0; i < damagedUnitList.Count; i++)
            {
                damagedUnitList[i].Damage(((ExplosiveShellType)BulletType).Damage);
            }
            var go = Instantiate(UnityEngine.Resources.Load(((ExplosiveShellType)BulletType).ExplosionPath) as GameObject, transform);
            go.transform.position += new Vector3(0, 0.5f, 0);
            go.transform.parent = null;
            go.transform.rotation = new Quaternion();
            rigidbody.velocity =new Vector3(0,0,0);
            lifetime = 10;
            dying = true;
        }
    }
}
