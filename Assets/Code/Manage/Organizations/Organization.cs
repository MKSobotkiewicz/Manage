using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Organizations
{
    public class Organization : Info
    {
        public HashSet<Organization> Enemies;

        public Organization(string name, string description, Texture2D flag, Color color, HashSet<Organization> enemies) :base(name,description,flag,color)
        {
            Enemies = enemies;
        }

    }
}
