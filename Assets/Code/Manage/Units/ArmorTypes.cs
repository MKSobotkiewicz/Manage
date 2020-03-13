
namespace Manage.Units
{
    public static class ArmorTypes
    {
        public enum EArmorType
        {
            SoldierCamoArmor,
            SoldierBlueArmor,
            SoldierJungleArmor,
            SoldierCamoAndJungleArmor,
            MercenaryBlackArmor,
            MercenaryOrangeArmor
        }

        public static ArmorType ToArmorType(EArmorType armorType)
        {
            switch (armorType)
            {
                case EArmorType.SoldierCamoArmor:
                    return SoldierCamoArmor;
                case EArmorType.SoldierBlueArmor:
                    return SoldierBlueArmor;
                case EArmorType.SoldierJungleArmor:
                    return SoldierJungleArmor;
                case EArmorType.SoldierCamoAndJungleArmor:
                    return SoldierCamoAndJungleArmor;
                case EArmorType.MercenaryBlackArmor:
                    return MercenaryBlackArmor;
                case EArmorType.MercenaryOrangeArmor:
                    return MercenaryOrangeArmor;
                default:
                    return null;
            }
        }

        public static readonly ArmorType SoldierCamoArmor = new ArmorType(
            "Camouflaged battle dress uniform", 
            "Standard battle dress uniform of the imperial armed forces. As there were many produced before the collapse, they still are abundant today.",
            "Units/Textures/Clothes_ItemIcon", 
            0, 
            0,
            0,
            2,
            0,
            0,
            "Units/soldier_camo_female", 
            "Units/soldier_camo_male");
        public static readonly ArmorType SoldierBlueArmor = new ArmorType(
            "Blue battle dress uniform",
            "Standard uniform of the imperial police. To this day, it's color is associated with law enforcement.",
            "Units/Textures/Clothes_ItemIcon", 
            0,
            0,
            0,
            2,
            0,
            0,
            "Units/soldier_blue_female", 
            "Units/soldier_blue_male");
        public static readonly ArmorType SoldierJungleArmor = new ArmorType(
            "Green battle dress uniform",
            "Plain green battle dress uniform. This older model of BDUs was used by second line units before the collapse, now various renegades still wear them.",
            "Units/Textures/Clothes_ItemIcon",
            0,
            0,
            0,
            2,
            0,
            0,
            "Units/soldier_jungle_female", 
            "Units/soldier_jungle_male");
        public static readonly ArmorType SoldierCamoAndJungleArmor = new ArmorType(
            "Camouflaged/green battle dress uniform",
            "A mix of two BDUs, one with camouflage and one plain green.",
            "Units/Textures/Clothes_ItemIcon",
            0,
            0,
            0,
            2,
            0,
            0,
            "Units/soldier_camoandjungle_female",
            "Units/soldier_camoandjungle_male");
        public static readonly ArmorType MercenaryBlackArmor = new ArmorType(
            "Black mercenary's clothes",
            "Nothing says that you're for hire like black leather jacket and black combat pants.",
            "Units/Textures/Clothes_ItemIcon",
            0,
            0,
            1,
            0,
            0,
            1,
            "Units/mercenary_black_female", 
            "Units/mercenary_black_male");
        public static readonly ArmorType MercenaryOrangeArmor = new ArmorType(
            "Orange mercenary's clothes",
            "Some mercenaries don't care as much about concealment as looking cool on the battlefield. And not many things are as cool as an orange leather jacket.",
            "Units/Textures/Clothes_ItemIcon",
            0,
            0,
            1,
            0,
            0,
            1,
            "Units/mercenary_orange_female",
            "Units/mercenary_orange_male");
    }
}
