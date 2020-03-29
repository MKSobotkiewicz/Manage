using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Units;

namespace Manage.Skills
{
    class HealingAura: Skill
    {
        private readonly float radius=30;
        private readonly string particlePath = "UI/Particles/HealingParticleSystem";

        public void Start()
        {
            reloadTime = 30;
        }

        public void Use(Vector3 position, List<Unit> affectedPlayerUnits)
        {
            foreach (var unit in affectedPlayerUnits)
            {
                if (Vector3.Distance(position, unit.Position()) <= radius)
                {
                    var go = Instantiate(UnityEngine.Resources.Load(particlePath) as GameObject, unit.transform);
                    go.transform.localPosition += new Vector3(0,1,0);
                    unit.Heal(unit.GetMaxHitPoints()/2);
                }
            }
        }
    }
}
