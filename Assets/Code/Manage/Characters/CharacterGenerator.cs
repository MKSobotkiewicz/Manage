using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Organizations;

namespace Manage.Characters
{
    static class CharacterGenerator
    {
        private static System.Random random = new Random();

        public static Character Generate(Organization organization)
        {
            return new Character(CharacterCultureTypes.BasicCulture, organization);
        }

        public static HashSet<Character> GenerateFamily(Organization organization, CharacterCulture culture)
        {
            var characters = new HashSet<Character>();

            var grandgrandpa = new Character(EGender.Male,EAge.VeryOld,organization,culture);
            var grandgrandma = new Character(EGender.Female, EAge.VeryOld, organization, culture);
            var grandgrandpas = new HashSet<Character> { grandgrandpa, grandgrandma };
            characters.Concat(grandgrandpas);

            for (int i = 1; i < random.Next(1, 8); i++)
            {
                Character grandpa, grandma;
                var gender = random.Next(0,1);
                if (gender == 0)
                {
                    grandpa = new Character(EGender.Male, grandgrandpas);
                    grandma = new Character(EGender.Female, EAge.Old, organization, culture);
                }
                else
                {
                    grandma = new Character(EGender.Female, grandgrandpas);
                    grandpa = new Character(EGender.Male,EAge.Old, organization, culture);
                }
                var grandpas = new HashSet<Character> { grandpa, grandma };
                characters.Concat(grandpas);

                for (int j = 1; j < random.Next(1, 8); j++)
                {
                    Character father, mother;
                    gender = random.Next(0, 1);
                    if (gender == 0)
                    {
                        father = new Character(EGender.Male, grandpas);
                        mother = new Character(EGender.Female, EAge.MiddleAged, organization, culture);
                    }
                    else
                    {
                        mother = new Character(EGender.Female, grandpas);
                        father = new Character(EGender.Male, EAge.MiddleAged, organization, culture);
                    }
                    var parents = new HashSet<Character> { mother, father };
                    characters.Concat(parents);
                }
            }

            return characters;
        }
    }
}
