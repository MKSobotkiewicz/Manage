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
        private static System.Random random = new System.Random();

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
                var damage = random.Next((int)(((ExplosiveShellType)BulletType).Damage * 0.5), (int)(((ExplosiveShellType)BulletType).Damage * 1.5));
                damagedUnitList[i].Damage(damage, Player, Player.Units[0]);
            }
            var go = Instantiate(UnityEngine.Resources.Load(((ExplosiveShellType)BulletType).ExplosionPath) as GameObject, transform);
            go.transform.position += new Vector3(0, 0.5f, 0);
            go.transform.parent = null;
            go.transform.rotation = new Quaternion();
            go.transform.Rotate(0, (float)random.NextDouble()*360, 0);
            rigidbody.velocity =new Vector3(0,0,0);
            lifetime = 10;
            dying = true;
        }
    }
}
