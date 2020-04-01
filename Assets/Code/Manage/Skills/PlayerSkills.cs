using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Skills
{
    public class PlayerSkills:List<Skill>
    {
        public void Add(Skill skill,Player.Player player)
        {
            Add(skill);
            skill.Player = player;
        }
    }
}
