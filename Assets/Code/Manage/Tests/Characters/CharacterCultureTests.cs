using Manage.Characters;
using Manage.Debug;
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
    public class CharacterCultureTests
    {
        [Test]
        public void RandomFirstNameTest()
        {
            var culture = GenerateCulture();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Female));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Female));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Female));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Female));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Female));

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Male));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Male));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Male));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Male));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomFirstName(EGender.Male));
        }
        
        [Test]
        public void RandomSurnameTest()
        {
            var culture = GenerateCulture();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomSurname());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomSurname());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomSurname());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomSurname());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, culture.RandomSurname());
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