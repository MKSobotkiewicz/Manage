using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Characters
{
    public class CharacterCulture
    {
        public string Name { get; private set; }

        public List<string> FemaleFirstNames { get; private set; }
        public List<string> MaleFirstNames { get; private set; }
        public List<string> Surnames { get; private set; }
        public List<string> FemaleNicknames { get; private set; }
        public List<string> MaleNicknames { get; private set; }
        public List<Texture2D> FemaleIcons { get; private set; }
        public List<Texture2D> MaleIcons { get; private set; }

        private System.Random random;

        public CharacterCulture(string name, 
                                List<string> femaleFirstNamesFiles,
                                List<string> maleFirstNamesFiles, 
                                List<string> surnamesFiles,
                                List<string> femaleNicknamesFiles,
                                List<string> maleNicknamesFiles,
                                List<string> femaleIconsFiles,
                                List<string> maleIconsFiles)
        {
            random = new System.Random();
            Name =name;
            FemaleFirstNames = new List<string>();
            MaleFirstNames = new List<string>();
            Surnames = new List<string>();
            FemaleNicknames = new List<string>();
            MaleNicknames = new List<string>();
            FemaleIcons = new List<Texture2D>();
            MaleIcons = new List<Texture2D>();
            using (var characterInfoParser = new CharacterInfoParser())
            {
                foreach (var firstNamesFile in femaleFirstNamesFiles)
                {
                    FemaleFirstNames.AddRange(characterInfoParser.ParseNames(firstNamesFile));
                }
                foreach (var firstNamesFile in maleFirstNamesFiles)
                {
                    MaleFirstNames.AddRange(characterInfoParser.ParseNames(firstNamesFile));
                }
                foreach (var surnamesFile in surnamesFiles)
                {
                    Surnames.AddRange(characterInfoParser.ParseNames(surnamesFile));
                }
                foreach (var femaleNicknamesFile in femaleNicknamesFiles)
                {
                    FemaleNicknames.AddRange(characterInfoParser.ParseNames(femaleNicknamesFile));
                }
                foreach (var maleNicknamesFile in maleNicknamesFiles)
                {
                    MaleNicknames.AddRange(characterInfoParser.ParseNames(maleNicknamesFile));
                }
                foreach (var femaleIconsFile in femaleIconsFiles)
                {
                    FemaleIcons.AddRange(characterInfoParser.ParseIcons(femaleIconsFile));
                }
                foreach (var maleIconsFile in maleIconsFiles)
                {
                    MaleIcons.AddRange(characterInfoParser.ParseIcons(maleIconsFile));
                }
            }
        }

        public string RandomFirstName(EGender gender)
        {
            switch (gender)
            {
                case EGender.Female:
                    return RandomFemaleFirstName();
                case EGender.Male:
                    return RandomMaleFirstName();
                default:
                    return RandomNeutralFirstName();
            }
        }

        public string RandomNickname(EGender gender)
        {
            switch (gender)
            {
                case EGender.Female:
                    return RandomFemaleNickname();
                case EGender.Male:
                    return RandomMaleNickname();
                default:
                    return RandomNeutralNickname();
            }
        }

        public Texture2D RandomIcon(EGender gender)
        {
            switch (gender)
            {
                case EGender.Female:
                    return RandomFemaleIcon();
                case EGender.Male:
                    return RandomMaleIcon();
                default:
                    return RandomNeutralIcon();
            }
        }

        public string RandomSurname()
        {
            return Surnames[random.Next(0, Surnames.Count)];
        }

        private string RandomFemaleFirstName()
        {
            return FemaleFirstNames[random.Next(0, FemaleFirstNames.Count)];
        }

        private string RandomMaleFirstName()
        {
            return MaleFirstNames[random.Next(0, MaleFirstNames.Count)];
        }

        private string RandomFemaleNickname()
        {
            if (random.Next(0, 2) == 0)
            {
                return FemaleNicknames[random.Next(0, FemaleNicknames.Count)];
            }
            return "";
        }

        private string RandomMaleNickname()
        {
            if (random.Next(0, 2) == 0)
            {
                return MaleNicknames[random.Next(0, MaleNicknames.Count)];
            }
            return "";
        }

        private string RandomNeutralFirstName()
        {
            var list = new List<string>();
            list.AddRange(FemaleFirstNames);
            list.AddRange(MaleFirstNames);
            return list[random.Next(0, list.Count)];
        }

        private string RandomNeutralNickname()
        {
            if (random.Next(0, 2) == 0)
            {
                var list = new List<string>();
                list.AddRange(FemaleNicknames);
                list.AddRange(MaleNicknames);
                return list[random.Next(0, list.Count)];
            }
            return "";
        }

        private Texture2D RandomFemaleIcon()
        {
            return FemaleIcons[random.Next(0, FemaleIcons.Count)];
        }

        private Texture2D RandomMaleIcon()
        {
            return MaleIcons[random.Next(0, MaleIcons.Count)];
        }

        private Texture2D RandomNeutralIcon()
        {
            var list = new List<Texture2D>();
            list.AddRange(FemaleIcons);
            list.AddRange(MaleIcons);
            return list[random.Next(0, list.Count)];
        }

    }
}
