using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Characters
{
    public class CharacterTraits : HashSet<CharacterTrait>
    {
        public uint Points{get;set; }

        private static Random random = new Random();

        public CharacterTraits():base()
        {
            Points = 0;
        }

        public CharacterTraits(uint level) : this()
        {
            Points = level /5;
            for (int i=0;i<Points;i++)
            {
                Add(CharacterTraitsList.Traits.ElementAt(random.Next(CharacterTraitsList.Traits.Count)));
            }
        }

        public new bool Add(CharacterTrait trait)
        {
            if (Points >= 0)
            {
                base.Add(trait);
                Points--;
                return true;
            }
            return false;
        }

        public new int Contains(CharacterTrait characterTrait)
        {
            return base.Contains(characterTrait) ? 1 : 0;
        }
    }
}
