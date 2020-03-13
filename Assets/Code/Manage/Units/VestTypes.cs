namespace Manage.Units
{
    public static class VestTypes
    {
        public enum EVestType
        {
            None,
            CamouflagedCombatVest,
            DesertCombatVest,
            SteelVest
        }

        public static VestType ToVestType(EVestType vestType)
        {
            switch (vestType)
            {
                case EVestType.CamouflagedCombatVest:
                    return CamouflagedCombatVest;
                case EVestType.DesertCombatVest:
                    return DesertCombatVest;
                case EVestType.SteelVest:
                    return SteelVest;
                default:
                    return null;
            }
        }

        public static readonly VestType CamouflagedCombatVest = new VestType(
            "Forest combat vest",
            "Combat vest used by the imperial armed forces. Effective against small caliber bullets. Forest camouflage variant.",
            "Units/Textures/Clothes_ItemIcon",
            15,
            0,
            0,
            0,
            0,
            0,
            "Units/Vests/camouflaged_combat_vest_female",
            "Units/Vests/camouflaged_combat_vest_male");

        public static readonly VestType DesertCombatVest = new VestType(
            "Desert combat vest",
            "Combat vest used by the imperial armed forces. Effective against small caliber bullets. Desert camouflage variant.",
            "Units/Textures/Clothes_ItemIcon",
            15,
            0,
            0,
            0,
            0,
            0,
            "Units/Vests/desert_combat_vest_female",
            "Units/Vests/desert_combat_vest_male");

        public static readonly VestType SteelVest = new VestType(
            "Steel vest",
            "Old and rusty steel vest. Mediocre, but still better than nothing. Painted green for better concealment.",
            "Units/Textures/Clothes_ItemIcon",
            10,
            0,
            0,
            0,
            0,
            0,
            "Units/Vests/steel_vest_female",
            "Units/Vests/steel_vest_male");
    }
}

