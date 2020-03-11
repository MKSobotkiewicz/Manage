using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Manage.Characters;
using Manage.Units;

namespace Manage.UI
{
    public class CreateCharacter : MonoBehaviour
    {
        public InputField FirstNameInputField;
        public InputField SurnameInputField;
        public InputField NicknameInputField;
        public InputField AgeInputField;
        public Button RandomizeFirstNameButton;
        public Button RandomizeSurnameButton;
        public Button RandomizeNicknameButton;
        public Button RandomizeAgeButton;
        public Button FemaleButton;
        public Button MaleButton;
        public Button OtherButton;
        private Texture2D Portrait;

        public CharacterCultureTypes.ECharacterCulture CharacterCulture;

        private EGender gender = EGender.Female;

        public void Start()
        {
            RandomizeFirstName();
            RandomizeSurname();
            RandomizeNickname();
            RandomizeAge();
            RandomizePortrait();
        }

        public void SetGenderFemale()
        {
            gender = EGender.Female;
        }
        public void SetGenderMale()
        {
            gender = EGender.Male;
        }
        public void SetGenderOther()
        {
            gender = EGender.Other;
        }

        public void RandomizeFirstName()
        {
            FirstNameInputField.text = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomFirstName(gender);
        }

        public void RandomizeSurname()
        {
            SurnameInputField.text = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomSurname();
        }

        public void RandomizeNickname()
        {
            NicknameInputField.text = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomNickname(gender);
        }

        public void RandomizeAge()
        {
            AgeInputField.text = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomNickname(gender);
        }

        public void RandomizePortrait()
        {
            Portrait = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomIcon(gender);
        }
    }
}
