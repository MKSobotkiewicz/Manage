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
        public UnitCreator UnitCreator;
        public UnitIds UnitIds;

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
        public Image FemaleSelectPanel;
        public Image MaleSelectPanel;
        public Image OtherSelectPanel;
        public RawImage CharacterImage;
        public Canvas ImagesCanvas;
        public RawImage CharacterPortraitPrefab;

        public CharacterCultureTypes.ECharacterCulture CharacterCulture;

        private Texture2D portrait;
        private EGender gender = EGender.Female;

        private static readonly System.Random random = new System.Random();

        public void Start()
        {
            SetGenderRandom();
            RandomizeFirstName();
            RandomizeSurname();
            RandomizeNickname();
            RandomizeAge();
            RandomizePortrait();
            SetImage(portrait);
        }

        public void Create()
        {
            FillUnitCreatorFields();
            UnitCreator.CreateAndDestroy();
            UnitIds.Reset();
            BasicUI basicUI;
            if ((basicUI = GetComponent<BasicUI>()) != null)
            {
                basicUI.Delete();
            }
            else
            {
                LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.1f).setOnComplete(Destroy).setEase(LeanTweenType.easeInCubic);
            }
        }

        public void SetGenderFemale()
        {
            gender = EGender.Female;
            FemaleSelectPanel.enabled = true;
            MaleSelectPanel.enabled = false;
            OtherSelectPanel.enabled = false;
            FillImagesCanvas();
        }

        public void SetGenderMale()
        {
            gender = EGender.Male;
            FemaleSelectPanel.enabled = false;
            MaleSelectPanel.enabled = true;
            OtherSelectPanel.enabled = false;
            FillImagesCanvas();
        }

        public void SetGenderOther()
        {
            gender = EGender.Other;
            FemaleSelectPanel.enabled = false;
            MaleSelectPanel.enabled = false;
            OtherSelectPanel.enabled = true;
            FillImagesCanvas();
        }

        public void SetImage(Texture2D _portrait)
        {
            portrait = _portrait;
            CharacterImage.texture = portrait;
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
            AgeInputField.text = Character.RandomAge().ToString();
        }

        public void RandomizePortrait()
        {
            portrait = CharacterCultureTypes.ToCharacterCulture(CharacterCulture).RandomIcon(gender);
        }

        private void FillUnitCreatorFields()
        {
            UnitCreator.FirstName = FirstNameInputField.text;
            UnitCreator.Surname = SurnameInputField.text;
            UnitCreator.Nickname = NicknameInputField.text;
            UnitCreator.Age = uint.Parse(AgeInputField.text);
            UnitCreator.Gender = gender;
            UnitCreator.Portrait = portrait;
            UnitCreator.CharacterCulture = CharacterCulture;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void FillImagesCanvas()
        {
            foreach (RawImage texture in ImagesCanvas.GetComponentsInChildren<RawImage>())
            {
                Destroy(texture.gameObject);
            }
            if (gender == EGender.Female|| gender == EGender.Other)
            {
                foreach (var texture in CharacterCultureTypes.ToCharacterCulture(CharacterCulture).FemaleIcons)
                {
                    var cp=Instantiate(CharacterPortraitPrefab, ImagesCanvas.transform);
                    cp.texture = texture;
                }
            }
            if (gender == EGender.Male || gender == EGender.Other)
            {
                foreach (var texture in CharacterCultureTypes.ToCharacterCulture(CharacterCulture).MaleIcons)
                {
                    var cp = Instantiate(CharacterPortraitPrefab, ImagesCanvas.transform);
                    cp.texture = texture;
                }
            }
        }

        private void SetGenderRandom()
        {
            var _random = random.Next(0, 100);
            if (_random < 48)
            {
                SetGenderFemale();
                return;
            }
            if (_random < 96)
            {
                SetGenderMale();
                return;
            }
            SetGenderOther();
            return;
        }
    }
}
