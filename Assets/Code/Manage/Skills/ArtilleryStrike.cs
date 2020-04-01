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
            Icon = UnityEngine.Resources.Load("UI/SkillsIcons/ArtilleryStrike") as Texture2D;
            ProjectorPrefab = UnityEngine.Resources.Load("UI/AreaEffects/ArtilleryStrikeAreaEffect") as GameObject;
            reloadTime = 60;
        }
    }
}
