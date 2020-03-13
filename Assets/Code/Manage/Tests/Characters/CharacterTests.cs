using Manage.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Manage.Characters.Tests
{
    [TestFixture]
    public class CharacterTests
    {
        [Test]
        public void CharacterTest1()
        {
            var culture = GenerateCulture();

            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
            new Character(culture, null);
        }

        private static CharacterCulture GenerateCulture()
        {
            return new CharacterCulture("culture1",
                                        new List<string> { "Assets/Resources/Names/FemaleFirstNamesSet1.xml" },
                                        new List<string> { "Assets/Resources/Names/MaleFirstNamesSet1.xml" },
                                        new List<string> { "Assets/Resources/Names/SurnamesSet1.xml" },
                                        new List<string> { "Assets/Resources/Names/FemaleNicknamesSet1.xml" },
                                        new List<string> { "Assets/Resources/Names/MaleNicknamesSet1.xml" },
                                        new List<string> { "Assets/Resources/Portraits/FemaleIconsSet1" },
                                        new List<string> { "Assets/Resources/Portraits/MaleIconsSet1" });
        }
    }
}