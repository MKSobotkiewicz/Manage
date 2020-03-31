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

        private Manage.Skills.Skill skill;

        public void Load(Manage.Skills.Skill _skill)
        {
            skill = _skill;
            UnityEngine.Debug.Log(skill.Icon);
            Icon.texture = skill.Icon;
        }
    }
}
