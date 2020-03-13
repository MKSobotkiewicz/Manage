using UnityEngine;

namespace Manage.Units
{
    public class HelmetType : ClothesType
    {
        public HelmetType(
            string name,
            string info,
            string iconPath,
            int value,
            uint stamina,
            uint endurance,
            uint marksmanship,
            uint cunning,
            uint charisma,
            string prefabPath) : base(
                name,
                info,
                iconPath,
                value,
                stamina,
                endurance,
                marksmanship,
                cunning,
                charisma,
                prefabPath,
                prefabPath)
        {
        }

        public static GameObject Load(HelmetType helmet, Transform transform)
        {
            return GameObject.Instantiate((UnityEngine.Resources.Load(helmet.MalePrefabPath) as GameObject), transform);
        }
    }
}
