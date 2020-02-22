using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Organizations
{
    public class Organization : Info
    {
        public Organization(string name, string description, Texture2D flag, Color color) :base(name,description,flag,color)
        {
        }

    }
}
