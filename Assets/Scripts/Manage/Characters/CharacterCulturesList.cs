using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Characters
{
    public static class CharacterCulturesList
    {
        public static readonly CharacterCulture BasicCulture = 
            new CharacterCulture("Basic culture",
                                 new List<string> { "Assets/Resources/Names/FemaleFirstNamesSet1.xml" },
                                 new List<string> { "Assets/Resources/Names/MaleFirstNamesSet1.xml" },
                                 new List<string> { "Assets/Resources/Names/SurnamesSet1.xml" },
                                 new List<string> { "Assets/Resources/Names/FemaleNicknamesSet1.xml" },
                                 new List<string> { "Assets/Resources/Names/MaleNicknamesSet1.xml" },
                                 new List<string> { "Assets/Resources/Portraits/FemaleIconsSet1" },
                                 new List<string> { "Assets/Resources/Portraits/MaleIconsSet1" });
    }
}
