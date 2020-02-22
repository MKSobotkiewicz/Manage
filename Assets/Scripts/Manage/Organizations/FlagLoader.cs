using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace Manage.Organizations
{
    public static class FlagLoader
    {
        public static Texture2D LoadFlag(string name)
        {
            var directory = new DirectoryInfo("Assets/Resources/Flags");
            var files = directory.GetFiles("*.png");
            var output= UnityEngine.Resources.Load<Texture2D>("Flags/"+name);
            if (Equals(output, null))
            {
                throw new FileLoadException("Can't load flag texture.");
            }
            return output;
        }
    }
}
