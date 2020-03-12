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
        public VehicleTypes.EVehicleType VehicleType;
        public OrganizationTypes.EOrganization Organization;
        public CharacterCultureTypes.ECharacterCulture CharacterCulture;
        [Range(1, 50)]
        public uint Level=1;
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

        public void Start()
        {
            if (!CreateOnStart)
            {
                return;
            }
            var unit =Create();
            if (Dialog != null)
            {
                var dialogManager = DialogManager.Create(Dialog, unit, MainCanvas);
            }
            Destroy(gameObject);
        }

        public Unit Create()
        {
            var Character = new Character(Level,
                                          Gender,
                                          OrganizationTypes.ToOrganization(Organization),
                                          CharacterCultureTypes.ToCharacterCulture(CharacterCulture),
                                          FirstName,
                                          Surname,
                                          Nickname,
                                          Age,
                                          Portrait);

            var unitFactory = new UnitFactory();
            return unitFactory.Create(Player,
                                      WeaponTypes.ToWeaponType(WeaponType),
                                      ArmorTypes.ToArmorType(ArmorType),
                                      HelmetTypes.ToHelmetType(HelmetType),
                                      VestTypes.ToVestType(VestType),
                                      VehicleTypes.ToVehicleType(VehicleType),
                                      Character,
                                      transform);
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
