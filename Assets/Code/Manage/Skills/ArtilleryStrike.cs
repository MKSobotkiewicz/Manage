using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Skills
{
    class ArtilleryStrike: Skill
    {
        public void Awake()
        {
            Icon = Instantiate(UnityEngine.Resources.Load("UI/SkillsIcons/ArtilleryStrike") as Texture2D);
            reloadTime = 60;
        }
    }
}
