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
        public BlackScreen BlackScreen;

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
        public Text PointsText;
        public Text StaminaText;
        public Text EnduranceText;
        public Text MarksmanshipText;
        public Text CunningText;
        public Text CharismaText;
        public Button AddStaminaButton;
        public Button AddEnduranceButton;
        public Button AddMarksmanshipButton;
        public Button AddCunningButton;
        public Button AddCharismaButton;
        public Button RemoveStaminaButton;
        public Button RemoveEnduranceButton;
        public Button RemoveMarksmanshipButton;
        public Button RemoveCunningButton;
        public Button RemoveCharismaButton;
        public Button CreateButton;
        public Text HitPointsText;
        public Text SpeedText;
        public Text WeaponSpreadText;
        public Text AbilitiesLoadTimeText;
        public Image FemaleSelectPanel;
        public Image MaleSelectPanel;
        public Image OtherSelectPanel;
        public RawImage CharacterImage;
        public Canvas ImagesCanvas;
        public RawImage CharacterPortraitPrefab;

        public CharacterCultureTypes.ECharacterCulture CharacterCulture;

        private Texture2D portrait;
        private EGender gender = EGender.Female;
        private uint points = 5;
        private uint stamina = 1;
        private uint endurance = 1;
        private uint marksmanship = 1;
        private uint cunning = 1;
        private uint charisma = 1;

        private static readonly System.Random random = new System.Random();

        public void Start()
        {
            SetGenderRandom();
            RandomizeFirstName();
            RandomizeSurname();
            RandomizeNickname();
            RandomizeAge();
            RandomizePortrait();
            CharacterImage.texture = portrait;
            UpdatePoints();
            UnitCreator.Level = 0;
        }

        public void Create()
        {
            FillUnitCreatorFields();
            UnitCreator.CreateAndDestroy();
            UnitIds.Reset();
            BlackScreen.Close();
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
            var aus = GetComponent<AudioSource>();
            if (aus != null)
            {
                aus.Play();
            }
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

        public void AddStamina()
        {
            points--;
            stamina++;
            UpdatePoints();
        }

        public void RemoveStamina()
        {
            points++;
            stamina--;
            UpdatePoints();
        }

        public void AddEndurance()
        {
            points--;
            endurance++;
            UpdatePoints();
        }

        public void RemoveEndurance()
        {
            points++;
            endurance--;
            UpdatePoints();
        }

        public void AddMarksmanship()
        {
            points--;
            marksmanship++;
            UpdatePoints();
        }

        public void RemoveMarksmanship()
        {
            points++;
            marksmanship--;
            UpdatePoints();
        }

        public void AddCunning()
        {
            points--;
            cunning++;
            UpdatePoints();
        }

        public void RemoveCunning()
        {
            points++;
            cunning--;
            UpdatePoints();
        }

        public void AddCharisma()
        {
            points--;
            charisma++;
            UpdatePoints();
        }

        public void RemoveCharisma()
        {
            points++;
            charisma--;
            UpdatePoints();
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

        private void UpdatePoints()
        {
            UnitCreator.Stamina = stamina;
            UnitCreator.Endurance = endurance;
            UnitCreator.Marksmanship = marksmanship;
            UnitCreator.Cunning = cunning;
            UnitCreator.Charisma = charisma;

            PointsText.text =  points.ToString();
            if (points != 0)
            {
                PointsText.color = new UnityEngine.Color(1, 1, 0, 1);
            }
            else
            {
                PointsText.color = new UnityEngine.Color(0, 0, 0, 1);
            }

            StaminaText.text= stamina.ToString();
            EnduranceText.text=endurance.ToString();
            MarksmanshipText.text=marksmanship.ToString();
            CunningText.text=cunning.ToString();
            CharismaText.text=charisma.ToString();

            HitPointsText.text = Unit.GetHitPoints(endurance).ToString();
            SpeedText.text = Unit.GetSpeed(stamina).ToString("0.00");
            WeaponSpreadText.text = ((int)(Unit.GetWeaponSpread(marksmanship) * 100)).ToString() + "%";
            AbilitiesLoadTimeText.text = ((int)(Unit.AbilitiesLoadTimeSingle(cunning) * 100)).ToString() + "%";

            if (points <= 0)
            {
                CreateButton.gameObject.SetActive(true);
                AddStaminaButton.gameObject.SetActive(false);
                AddEnduranceButton.gameObject.SetActive(false);
                AddMarksmanshipButton.gameObject.SetActive(false);
                AddCunningButton.gameObject.SetActive(false);
                AddCharismaButton.gameObject.SetActive(false);
            }
            else
            {
                CreateButton.gameObject.SetActive(false);
                AddStaminaButton.gameObject.SetActive(true);
                AddEnduranceButton.gameObject.SetActive(true);
                AddMarksmanshipButton.gameObject.SetActive(true);
                AddCunningButton.gameObject.SetActive(true);
                AddCharismaButton.gameObject.SetActive(true);
            }

            if (stamina <= 1)
            {
                RemoveStaminaButton.gameObject.SetActive(false);
            }
            else
            {
                RemoveStaminaButton.gameObject.SetActive(true);
            }

            if (endurance <= 1)
            {
                RemoveEnduranceButton.gameObject.SetActive(false);
            }
            else
            {
                RemoveEnduranceButton.gameObject.SetActive(true);
            }

            if (marksmanship <= 1)
            {
                RemoveMarksmanshipButton.gameObject.SetActive(false);
            }
            else
            {
                RemoveMarksmanshipButton.gameObject.SetActive(true);
            }

            if (cunning <= 1)
            {
                RemoveCunningButton.gameObject.SetActive(false);
            }
            else
            {
                RemoveCunningButton.gameObject.SetActive(true);
            }

            if (charisma <= 1)
            {
                RemoveCharismaButton.gameObject.SetActive(false);
            }
            else
            {
                RemoveCharismaButton.gameObject.SetActive(true);
            }
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
