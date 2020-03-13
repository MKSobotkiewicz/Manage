using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Units
{
    public class ItemType
    {
        public string Name { get; private set; }
        public string Info { get; private set; }
        public string IconPath { get; private set; }

        public ItemType(string name, string info, string iconPath)
        {
            Info = info;
            Name = name;
            IconPath = iconPath;
        }

        public Texture2D Icon()
        {
            return GameObject.Instantiate(UnityEngine.Resources.Load<Texture2D>(IconPath)) as Texture2D;
        }
    }
}
