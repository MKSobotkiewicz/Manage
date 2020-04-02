using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class ExplosiveShellType:BulletType
    {
        public string ExplosionPath { get; private set; }
        public int Radius { get; private set; }

        public ExplosiveShellType(string prefab,
                                  string muzzleFlashPrefabPath,
                                  double velocity,
                                  double mass,
                                  int damage,
                                  int piercing,
                                  int radius,
                                  string explosionPath) : base(prefab,
                                                       muzzleFlashPrefabPath,
                                                       velocity,
                                                       mass,
                                                       damage,
                                                       piercing)
        {
            Radius = radius;
            ExplosionPath = explosionPath;
        }

        public new ExplosiveShell CreateBullet(Unit unit, Vector3 position, Vector3 target)
        {
            return (ExplosiveShell)ExplosiveShell.Create(this, unit, position, target);
        }
    }
}
