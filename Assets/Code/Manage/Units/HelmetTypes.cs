namespace Manage.Units
{
    public static class HelmetTypes
    {
        public enum EHelmetType
        {
            None,
            CamouflagedCombatHelmet,
            DesertCombatHelmet,
            SteelHelmet
        }

        public static HelmetType ToHelmetType(EHelmetType helmetType)
        {
            switch (helmetType)
            {
                case EHelmetType.CamouflagedCombatHelmet:
                    return CamouflagedCombatHelmet;
                case EHelmetType.DesertCombatHelmet:
                    return DesertCombatHelmet;
                case EHelmetType.SteelHelmet:
                    return SteelHelmet;
                default:
                    return null;
            }
        }

        public static readonly HelmetType CamouflagedCombatHelmet = new HelmetType(
            "Forest combat helmet",
            "Combat helmet used by the imperial armed forces. Effective against small caliber bullets. Forest camouflage variant.",
            "Units/Textures/Clothes_ItemIcon",
            5,
            0,
            0,
            0,
            0,
            0,
            "Units/Helmets/camouflaged_combat_helmet");

        public static readonly HelmetType DesertCombatHelmet = new HelmetType(
            "Desert combat helmet",
            "Combat helmet used by the imperial armed forces. Effective against small caliber bullets. Desert camouflage variant.",
            "Units/Textures/Clothes_ItemIcon",
            5,
            0,
            0,
            0,
            0,
            0,
            "Units/Helmets/desert_combat_helmet");

        public static readonly HelmetType SteelHelmet = new HelmetType(
            "Steel helmet",
            "Old and rusty steel helmet. Mediocre, but still better than nothing. Painted green for better concealment.",
            "Units/Textures/Clothes_ItemIcon",
            3,
            0,
            0,
            0,
            0,
            0,
            "Units/Helmets/steel_helmet");
    }
}
