using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Skills
{
    public class SkillFactory
    {
        public enum ESkillType
        {
            ArtilleryStrike,
            HealingAura
        }

        private List<Skill> skillsList = new List<Skill>();

        public Skill Create(ESkillType skillType,Player.Player player)
        {
            var go = new GameObject();
            Skill skill;
            switch (skillType)
            {
                case ESkillType.ArtilleryStrike:
                    skill = go.AddComponent<ArtilleryStrike>();
                    break;
                case ESkillType.HealingAura:
                    skill = go.AddComponent<HealingAura>();
                    break;
                default:
                    return null;
            }
            skill.Player = player;
            skillsList.Add(skill);
            return skill;
        }
    }
}
