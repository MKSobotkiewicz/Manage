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
    public class Player : MonoBehaviour
    {
        public Organization Organization { get; private set; }

        public List<Unit> Units { get; private set; }
        public PlayerInventory PlayerInventory { get;private set;}
        public PlayerSkills PlayerSkills { get; private set; }
        public ExperienceManager ExperienceManager { get; private set; }

        public void Awake()
        {
            Units = new List<Unit>();
            PlayerSkills = new PlayerSkills();
            PlayerInventory = new PlayerInventory();
            ExperienceManager = new ExperienceManager(this);

            Organization = OrganizationTypes.Empire;

            var skillFactory = new SkillFactory();
            PlayerSkills.Add(skillFactory.Create(SkillFactory.ESkillType.ArtilleryStrike,this));
            PlayerSkills.Add(skillFactory.Create(SkillFactory.ESkillType.HealingAura, this));
        }
    }
}
