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
    }
}
