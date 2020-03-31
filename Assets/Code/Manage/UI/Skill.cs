using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class Skill : MonoBehaviour
    {
        public RawImage Icon;

        private Skills.Skill skill;

        public void Load(Skills.Skill _skill)
        {
            skill = _skill;
            Icon.texture = skill.Icon;
        }
    }
}
