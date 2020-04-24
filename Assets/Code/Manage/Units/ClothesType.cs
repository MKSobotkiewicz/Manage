using UnityEngine;

namespace Manage.Units
{
    public class ClothesType : ItemType
    {
        public int Value { get; private set; }
        public uint Stamina { get; private set; }
        public uint Endurance { get; private set; }
        public uint Marksmanship { get; private set; }
        public uint Command { get; private set; }
        public uint Charisma { get; private set; }
        public string FemalePrefabPath { get; private set; }
        public string MalePrefabPath { get; private set; }

        public ClothesType(string name,
                          string info,
                          string iconPath,
                          int value,
                          uint stamina,
                          uint endurance,
                          uint marksmanship,
                          uint command,
                          uint charisma,
                          string femalePrefabPath,
                          string malePrefabPath) : base(name, info, iconPath)
        {
            Value = value;
            Stamina = stamina;
            Endurance = endurance;
            Marksmanship = marksmanship;
            Command = command;
            Charisma = charisma;
            FemalePrefabPath = femalePrefabPath;
            MalePrefabPath = malePrefabPath;
        }

        public static GameObject Load(ClothesType armor, Transform transform, Characters.EGender Gender)
        {
            if (Gender == Characters.EGender.Female)
            {
                return GameObject.Instantiate((UnityEngine.Resources.Load(armor.FemalePrefabPath) as GameObject), transform);
            }
            return GameObject.Instantiate((UnityEngine.Resources.Load(armor.MalePrefabPath) as GameObject), transform);
        }
    }
}
