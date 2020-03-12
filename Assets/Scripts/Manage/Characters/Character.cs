using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Organizations;
using UnityEngine;

namespace Manage.Characters
{
    public class Character : CharacterInfo
    {
        public uint Level { get; private set; }

        public CharacterStats CharacterStats { get; set; }
        public CharacterTraits CharacterTraits { get; set; }

        public HashSet<Character> Parents { get; set; }
        public HashSet<Character> Siblings { get; set; }
        public HashSet<Character> Children { get; set; }

        private static readonly System.Random random=new System.Random();

        public Character(uint level,
                         EGender gender,
                         Organization organization,
                         CharacterCulture characterCulture,
                         string firstName,
                         string surname,
                         string nickname,
                         uint age,
                         Texture2D portrait) : base(organization,
                                                    characterCulture,
                                                    firstName,
                                                    surname,
                                                    nickname,
                                                    age,
                                                    gender,
                                                    portrait)
        {
            Level = level;
            CharacterStats = new CharacterStats(Level);
            CharacterTraits = new CharacterTraits(Level);
            Parents = new HashSet<Character>();
            Siblings = new HashSet<Character>();
            Children = new HashSet<Character>();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Character named: " + firstName + " "
                                                                + surname + " age: " + age + " gender: " + gender + " created.");
        }

        public Character(EGender gender, 
                         Organization organization,
                         CharacterCulture characterCulture,
                         string firstName,
                         string surname,
                         string nickname,
                         uint age,
                         Texture2D portrait) : base(organization,
                                                    characterCulture,
                                                    firstName,
                                                    surname,
                                                    nickname,
                                                    age,
                                                    gender, 
                                                    portrait)
        {
            CharacterStats = new CharacterStats();
            CharacterTraits = new CharacterTraits();
            Parents = new HashSet<Character>();
            Siblings = new HashSet<Character>();
            Children = new HashSet<Character>();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Character named: " + firstName +" "
                                                                + surname +" age: "+age+ " gender: "+gender+" created.");
        }

        public Character(EGender gender,
                         Organization organization,
                         string firstName,
                         string surname,
                         string nickname,
                         uint age,
                         Texture2D portrait,
                         HashSet<Character> parents) : this(gender,
                                                            organization,
                                                            parents.ElementAt(0).CharacterCulture,
                                                            firstName,
                                                            surname,
                                                            nickname,
                                                            age,
                                                            portrait)
        {
            Parents = parents;
            foreach (var parent in parents)
            {
                if (parent.Age - Age < 16)
                {
                    continue;
                }
                parent.Children.Add(this);
                Siblings=Siblings.Concat(parent.Children) as HashSet<Character>;
            }
        }

        public Character(HashSet<Character> parents) : this(RandomGender(),
                                                            parents)
        {
        }

        public Character(CharacterCulture culture,Organization organization) : this( RandomGender(), organization, culture)
        {
        }

        public Character(CharacterCulture culture, Organization organization, EAge age) : this(RandomGender(),age, organization, culture)
        {
        }

        public Character(EGender gender,
                         HashSet<Character> parents) : this(gender,
                                                            parents.ElementAt(0).Organization,
                                                            parents.ElementAt(0).CharacterCulture,
                                                            parents.ElementAt(0).CharacterCulture.RandomFirstName(gender),
                                                            parents.ElementAt(new System.Random().Next(0,2)).Surname,
                                                            parents.ElementAt(0).CharacterCulture.RandomNickname(gender),
                                                            RandomAge(parents),
                                                            parents.ElementAt(0).CharacterCulture.RandomIcon(gender))
        {
            if (Age != 0)
            {
                Parents = parents;
                foreach (var parent in parents)
                {
                    if (parent.Age - Age < 16)
                    {
                        continue;
                    }
                    parent.Children.Add(this);
                    Siblings = Siblings.Concat(parent.Children) as HashSet<Character>;
                }
            }
            else
            {
                Age = RandomAge();
            }
        }

        public Character(EGender gender,
                         Organization organization,
                         CharacterCulture culture) : this(gender, 
                                                          organization,
                                                          culture,
                                                          culture.RandomFirstName(gender),
                                                          culture.RandomSurname(),
                                                          culture.RandomNickname(gender),
                                                          RandomAge(),
                                                          culture.RandomIcon(gender))
        {
        }

        public Character(EGender gender,
                         EAge age,
                         Organization organization,
                         CharacterCulture culture) : this(gender,
                                                          organization,
                                                          culture,
                                                          culture.RandomFirstName(gender),
                                                          culture.RandomSurname(),
                                                          culture.RandomNickname(gender),
                                                          RandomAge(age),
                                                          culture.RandomIcon(gender))
        {
        }

        public Character(EGender gender, CharacterCulture culture, Organization organization, uint level) : this(level,
                                                                                                                 gender,
                                                                                                                 organization,
                                                                                                                 culture,
                                                                                                                 culture.RandomFirstName(gender),
                                                                                                                 culture.RandomSurname(),
                                                                                                                 culture.RandomNickname(gender),
                                                                                                                 RandomAge(),
                                                                                                                 culture.RandomIcon(gender))
        {
        }

        public Character(CharacterCulture culture, Organization organization, uint level) : this(RandomGender(),
                                                                                                 culture,
                                                                                                 organization, 
                                                                                                 level)
        {
        }

        public void AddLevel()
        {
            Level++;
            CharacterStats.Points++;
            if (Level % 5 == 0)
            {
                CharacterTraits.Points++;
            }
        }

        private static uint RandomAge(HashSet<Character> parents)
        {
            if (parents != null)
            {
                int max = Math.Min((int)parents.ElementAt(0).Age,
                                   (int)parents.ElementAt(1).Age) - 16;
                int min = Math.Min((int)parents.ElementAt(0).Age,
                                   (int)parents.ElementAt(1).Age) - 29;
                if (max > 16)
                {
                    return (uint) random.Next(16, max);
                }
                return 0;
            }
            return RandomAge();
        }

        public static uint RandomAge()
        {
            return (uint)random.Next(16, 100);
        }

        private static uint RandomAge(EAge age)
        {
            switch (age)
            {
                case EAge.Young:
                    return (uint)random.Next(16, 37);
                case EAge.MiddleAged:
                    return (uint)random.Next(38, 58);
                case EAge.Old:
                    return (uint)random.Next(59, 79);
                case EAge.VeryOld:
                    return (uint)random.Next(80, 100);
                default: return 0;

            }
        }

        public static EGender RandomGender()
        {
            var _random = random.Next(0,100);
            if (_random < 48)
            {
                return EGender.Female;
            }
            if (_random < 96)
            {
                return EGender.Male;
            }
            return EGender.Other;
        }
    }
}
