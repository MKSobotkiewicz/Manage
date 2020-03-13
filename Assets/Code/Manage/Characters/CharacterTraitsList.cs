using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Characters
{
    public static class CharacterTraitsList
    {
        public static readonly CharacterTrait Tough =
            new CharacterTrait ("Tough",
                                "Character is so tough that he has extra 5 armor.");
        public static readonly CharacterTrait Unyielding =
            new CharacterTrait("Unyielding",
                               "When hit character staggers for around 1 instead of 3 seconds.");
        public static readonly CharacterTrait Perceptive =
            new CharacterTrait("Perceptive",
                               "Have 35% higher maximum weapon range.");
        public static readonly CharacterTrait FastShooter =
            new CharacterTrait("Fast Shooter",
                               "Character shoots 25% faster.");
        public static readonly CharacterTrait Healthy =
            new CharacterTrait("Healthy",
                               "Character heals twice as fast.");
        public static readonly CharacterTrait Grenadier =
            new CharacterTrait("Grenadier",
                               "Character throws grenades every 15 seconds instead of 25.");

        public static readonly List<CharacterTrait> Traits = new List<CharacterTrait> { Tough,
                                                                                        Unyielding,
                                                                                        Perceptive,
                                                                                        FastShooter,
                                                                                        Healthy,
                                                                                        Grenadier };
    }
}
