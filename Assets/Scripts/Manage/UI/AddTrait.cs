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
    class AddTrait: MonoBehaviour
    {
        public List<Text> Traits;
        public List<Image> SelectPanel;
        public Text TraitInfoText;

        private Unit unit;
        private Text selectedTrait;
        private Image SelectedPanel;
        private CharacterTrait characterTrait;
        private CharacterId characterId;

        public AddTrait(Unit _unit)
        {
            unit = _unit;
        }

        public void FillFields(Unit _unit, CharacterId _characterId)
        {
            unit = _unit;
            characterId = _characterId;
            if (_unit == null)
            {
                throw new NullReferenceException();
            }
            foreach (var trait in CharacterTraitsList.Traits)
            {
                if (unit.Character.CharacterTraits.Contains(trait) == 1)
                {
                    continue;
                }
                foreach (var traitText in Traits)
                {
                    if (traitText.text != "")
                    {
                        continue;
                    }
                    traitText.text = trait.Name;
                    break;
                }
            }
            Select(0);
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

        public void Select(int i)
        {
            UnityEngine.Debug.Log("Selected "+i.ToString());
            if (SelectedPanel != null)
            {
                SelectedPanel.enabled = false;
            }
            SelectedPanel = SelectPanel[i];
            selectedTrait = Traits[i];
            SelectedPanel.enabled = true;
            foreach (var trait in CharacterTraitsList.Traits)
            {
                if (trait.Name == selectedTrait.text)
                {
                    characterTrait = trait;
                }
            }
            TraitInfoText.text = characterTrait.Info;
        }

        public void Add()
        {
            unit.Character.CharacterTraits.Add(characterTrait);
            UnityEngine.Debug.Log("Adding " + characterTrait.Name+" to "+ unit.Character.FirstName+" "+ unit.Character.Surname);
            characterId.UpdateTraits();
            Delete();
        }

    }
}
