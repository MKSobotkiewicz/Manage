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
        private readonly float radius=5;
        private readonly string particlePath = "UI/Particles/HealingParticleSystem";

        public void Awake()
        {
            Icon = UnityEngine.Resources.Load("UI/SkillsIcons/HealingAura") as Texture2D;
            ProjectorPrefab = UnityEngine.Resources.Load("UI/AreaEffects/HealingAreaEffect") as GameObject;
            reloadTime = 30;
        }

        public void Use(Vector3 position, List<Unit> affectedPlayerUnits)
        {
            int i = 0;
            foreach (var unit in affectedPlayerUnits)
            {
                if (Vector3.Distance(position, unit.Position()) <= radius)
                {
                    i++;
                    var go = Instantiate(UnityEngine.Resources.Load(particlePath) as GameObject, unit.transform);
                    go.transform.localPosition += new Vector3(0,1,0);
                    unit.Heal(unit.GetMaxHitPoints()/2);
                }
            }
            if (i <= 0)
            {
                Timer = 0.1f;
            }
        }
    }
}
