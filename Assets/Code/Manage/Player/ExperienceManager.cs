using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Organizations;
using Manage.Units;
using Manage.Skills;

namespace Manage.Player
{
    public class ExperienceManager
    {
        private readonly Player player;

        private readonly string particlePath = "UI/Particles/LevelUpParticleSystem";

        public ExperienceManager(Player _player)
        {
            player = _player;
        }

        public void AddExperienceToUnits(uint experience)
        {
            foreach (var unit in player.Units)
            {
                if (unit.Character.AddExperience(experience))
                {
                    var go = GameObject.Instantiate(UnityEngine.Resources.Load(particlePath) as GameObject, unit.transform);
                    go.transform.localPosition += new Vector3(0, 1, 0);
                }
            }
        }
    }
}
