using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Organizations;
using Manage.Dialog;

namespace Manage.Units
{
    public class UnitCreator: MonoBehaviour
    {
        public bool CreateOnStart = true;
        public Player.Player Player;
        public Canvas MainCanvas;
        public WeaponTypes.EWeaponType WeaponType;
        public ArmorTypes.EArmorType ArmorType;
        public HelmetTypes.EHelmetType HelmetType;
        public VestTypes.EVestType VestType;
        public GrenadeTypes.EGrenadeType GrenadeType;
        public VehicleTypes.EVehicleType VehicleType;
        public OrganizationTypes.EOrganization Organization;
        public CharacterCultureTypes.ECharacterCulture CharacterCulture;
        [Range(0, 50)]
        public uint Level=1;
        [Range(1, 50)]
        public uint Stamina = 1;
        [Range(1, 50)]
        public uint Endurance = 1;
        [Range(1, 50)]
        public uint Marksmanship = 1;
        [Range(1, 50)]
        public uint Cunning = 1;
        [Range(1, 50)]
        public uint Charisma = 1;
        public EGender Gender= EGender.Male;
        [ContextMenuItem("Randomize first name", "RandomizeFirstName")]
        public string FirstName="John";
        [ContextMenuItem("Randomize surname", "RandomizeSurname")]
        public string Surname="Doe";
        [ContextMenuItem("Randomize nickname", "RandomizeNickname")]
        public string Nickname="";
        [Range(18, 99)]
        public uint Age=25;
        [ContextMenuItem("Randomize portrait", "RandomizePortrait")]
        public Texture2D Portrait;
        public Dialog.Dialog Dialog;
        public AddUnitsToPlayer AddUnitsToPlayer;

        public void Start()
        {
            if (!CreateOnStart)
            {
                return;
            }
            CreateAndDestroy();
        }

        public void CreateAndDestroy()
        {
            var unit = Create();
            if (Dialog != null)
            {
                var dialogManager = DialogManager.Create(Dialog, unit, MainCanvas);
            }
            Destroy(gameObject);
        }

        public Unit Create()
        {
            Character Character;
            if (Level == 0)
            {
                Character = new Character(Stamina,
                                          Endurance,
                                          Marksmanship,
                                          Cunning,
                                          Charisma,
                                          Gender,
                                          OrganizationTypes.ToOrganization(Organization),
                                          CharacterCultureTypes.ToCharacterCulture(CharacterCulture),
                                          FirstName,
                                          Surname,
                                          Nickname,
                                          Age,
                                          Portrait);
            }
            else
            {
                Character = new Character(Level,
                                          Gender,
                                          OrganizationTypes.ToOrganization(Organization),
                                          CharacterCultureTypes.ToCharacterCulture(CharacterCulture),
                                          FirstName,
                                          Surname,
                                          Nickname,
                                          Age,
                                          Portrait);
            }

            var unitFactory = new UnitFactory();
            var unit= unitFactory.Create(Player,
                                         WeaponTypes.ToWeaponType(WeaponType),
                                         ArmorTypes.ToArmorType(ArmorType),
                                         HelmetTypes.ToHelmetType(HelmetType),
                                         VestTypes.ToVestType(VestType),
                                         GrenadeTypes.ToGrenadeType(GrenadeType),
                                         VehicleTypes.ToVehicleType(VehicleType),
                                         Character,
                                         transform);
            if (AddUnitsToPlayer != null)
            {
                AddUnitsToPlayer.Units.Add(unit);
            }
            return unit;
        }

        private void RandomizeFirstName()
        {
            FirstName = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomFirstName(Gender);
        }

        private void RandomizeSurname()
        {
            Surname = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomSurname();
        }

        private void RandomizeNickname()
        {
            Nickname = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomNickname(Gender);
        }

        private void RandomizePortrait()
        {
            Portrait = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomIcon(Gender);
        }
    }
}
