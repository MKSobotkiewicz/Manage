using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manage.Organizations;
using UnityEngine;

namespace Manage.Characters
{
    public class CharacterInfo
    {
        public CharacterCulture CharacterCulture { get; private set; }
        public Organization Organization { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Nickname { get; private set; }
        public uint Age { get; protected set; }
        public EGender Gender { get; protected set; }
        public Texture2D Portrait { get; private set; }

        public CharacterInfo(Organization organization,
                             CharacterCulture characterCulture,
                             string firstName,
                             string surname,
                             string nickname,
                             uint age,
                             EGender gender,
                             Texture2D portrait)
        {
            Organization = organization;
            CharacterCulture =characterCulture;
            FirstName = firstName;
            Surname = surname;
            Nickname = nickname;
            Age = age;
            Gender = gender;
            Portrait = portrait;
        }

        public new string ToString()
        {
            return FirstName+" "+Surname+", "+Gender.ToString()+", Aged "+Age.ToString();
        }
    }

    public enum EGender
    {
        Male,
        Female,
        Other
    }

    public enum EAge
    {
        Young,
        MiddleAged,
        Old,
        VeryOld
    }
}
