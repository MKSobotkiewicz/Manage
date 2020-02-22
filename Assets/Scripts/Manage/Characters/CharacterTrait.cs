using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Characters
{
    public class CharacterTrait
    {
        public string Name { get; private set; }
        public string Info { get; private set; }

        public CharacterTrait(string name,string info)
        {
            Name = name;
            Info=info;
        }
    }
}
