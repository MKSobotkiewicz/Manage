﻿
namespace Manage.Units
{
    public class ArmorType: ClothesType
    {
        public ArmorType(
            string name, 
            string info,
            string iconPath,
            int value,
            uint stamina,
            uint endurance,
            uint marksmanship,
            uint cunning,
            uint charisma,
            string femalePrefabPath, 
            string malePrefabPath) : base(
                name,
                info, 
                iconPath, 
                value,
                stamina, 
                endurance,
                marksmanship,
                cunning,
                charisma,
                femalePrefabPath,
                malePrefabPath)
        {
        }
    }
}