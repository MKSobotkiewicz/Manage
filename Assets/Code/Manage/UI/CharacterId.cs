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
    public class CharacterId : MonoBehaviour
    {
        public GameObject AddTraitPrefab;
        public List<GameObject> AddStatButtons;
        public GameObject AddTraitButton;

        public GameObject WeaponCanvasPrefab;
        public GameObject ArmorCanvasPrefab;
        public GameObject HelmetCanvasPrefab;
        public GameObject VestCanvasPrefab;

        public ItemSlot WeaponSlot;
        public ItemSlot ArmorSlot;
        public ItemSlot HelmetSlot;
        public ItemSlot VestSlot;

        private Unit unit;
        private Player.Player player;

        private UnitId unitId;
        private Canvas inventoryCanvas;
        private Text firstName;
        private Text surname;
        private Text nickname;
        private Text gender;
        private Text age;
        private Text affiliation;
        private Text stamina;
        private Text endurance;
        private Text marksmanship;
        private Text cunning;
        private Text charisma;
        private Text level;
        private HashSet<Text> traits;
        private RawImage flag;
        private RawImage portrait;

        public CharacterId(Unit _unit, Player.Player _player, UnitId _unitId)
        {
            unit = _unit;
            player = _player;
            unitId = _unitId;
        }

        public void Update()
        {
            
        }

        public void FillFields(Unit _unit, Player.Player _player, UnitId _unitId)
        {
            AssignPrivates();
            unit = _unit;
            player = _player;
            unitId = _unitId;
            if (_unit == null)
            {
                throw new NullReferenceException();
            }
            firstName.text = unit.Character.FirstName;
            surname.text = unit.Character.Surname;
            if (unit.Character.Nickname != "")
            {
                nickname.text = "\"" + unit.Character.Nickname + "\"";
            }
            else
            {
                nickname.text = "N/A";
            }
            gender.text = unit.Character.Gender.ToString();
            age.text = unit.Character.Age.ToString();
            if (!Equals(unit.Character.Organization, null))
            {
                affiliation.text = unit.Character.Organization.Name;
                flag.texture = unit.Character.Organization.Flag;
            }
            else
            {
                affiliation.text = "None";
            }
            portrait.texture = unit.Character.Portrait;

            level.text = "LEVEL "+unit.Character.Level.ToString();

            SetStats();
            UpdateTraits();
            CheckStatPoints();
            CheckTraitPoints();
            FillInventory();
            FillEquipment();
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

        public void SetStats()
        {

            stamina.text = unit.Character.CharacterStats.GetStamina().ToString();
            if (unit.Inventory.ArmorType.Stamina > 0)
            {
                stamina.text += "+" + unit.Inventory.GetStamina();
            }

            endurance.text = unit.Character.CharacterStats.GetEndurance().ToString();
            if (unit.Inventory.ArmorType.Endurance > 0)
            {
                endurance.text += "+" + unit.Inventory.GetEndurance();
            }

            marksmanship.text = unit.Character.CharacterStats.GetMarksmanship().ToString();
            if (unit.Inventory.ArmorType.Marksmanship > 0)
            {
                marksmanship.text += "+" + unit.Inventory.GetMarksmanship();
            }

            cunning.text = unit.Character.CharacterStats.GetCunning().ToString();
            if (unit.Inventory.ArmorType.Cunning > 0)
            {
                cunning.text += "+" + unit.Inventory.GetCunning();
            }

            charisma.text = unit.Character.CharacterStats.GetCharisma().ToString();
            if (unit.Inventory.ArmorType.Charisma > 0)
            {
                charisma.text += "+" + unit.Inventory.GetCharisma();
            }
        }

        public void AddStamina()
        {
            stamina.text = unit.Character.CharacterStats.AddStamina().ToString();
            if (unit.Inventory.ArmorType.Stamina > 0)
            {
                stamina.text += "+" + unit.Inventory.GetStamina();
            }
            CheckStatPoints();
        }

        public void AddMarksmanship()
        {
            marksmanship.text = unit.Character.CharacterStats.AddMarksmanship().ToString();
            if (unit.Inventory.ArmorType.Marksmanship > 0)
            {
                marksmanship.text += "+" + unit.Inventory.GetMarksmanship();
            }
            CheckStatPoints();
        }

        public void AddEndurance()
        {
            endurance.text = unit.Character.CharacterStats.AddEndurance().ToString();
            if (unit.Inventory.ArmorType.Endurance > 0)
            {
                endurance.text += "+" + unit.Inventory.GetEndurance();
            }
            CheckStatPoints();
        }

        public void AddCunning()
        {
            cunning.text = unit.Character.CharacterStats.AddCunning().ToString();
            if (unit.Inventory.ArmorType.Cunning > 0)
            {
                cunning.text += "+" + unit.Inventory.GetCunning();
            }
            CheckStatPoints();
        }

        public void AddCharisma()
        {
            charisma.text = unit.Character.CharacterStats.AddCharisma().ToString();
            if (unit.Inventory.ArmorType.Charisma > 0)
            {
                charisma.text += "+" + unit.Inventory.GetCharisma();
            }
            CheckStatPoints();
        }

        public void AddTrait()
        {
            var go = Instantiate(AddTraitPrefab, transform);
            var addTrait = go.GetComponent<AddTrait>();
            addTrait.FillFields(unit,this);
            addTrait.transform.SetParent(transform.parent);
        }

        public void UpdateTraits()
        {
            for (int i = 0; i < unit.Character.CharacterTraits.Count; i++)
            {
                traits.ToList()[i].text = unit.Character.CharacterTraits.ToList()[i].Name;
            }
            CheckTraitPoints();
        }

        public void Rearm(WeaponType weaponType)
        {
            unit.Rearm(weaponType);
        }

        public void ChangeArmor(ArmorType armorType)
        {
            unit= unit.ChangeArmor(unit, armorType);
            unitId.Unit = unit;
        }

        public void ChangeHelmet(HelmetType helmetType)
        {
            unit.PutOnHelmet(helmetType);
        }

        public void ChangeVest(VestType vestType)
        {
            unit.PutOnVest(vestType);
        }

        public void UpdateInventory()
        {
            player.PlayerInventory.Clear();
            foreach (var item in inventoryCanvas.transform.GetComponentsInChildren<Item>())
            {
                player.PlayerInventory.Add(item.ItemType);
            }
            SetStats();
        }

        private void CheckStatPoints()
        {
            if (unit.Character.CharacterStats.Points <= 0)
            {
                foreach (var button in AddStatButtons)
                {
                    button.SetActive(false);
                }
            }
            else
            {
                foreach (var button in AddStatButtons)
                {
                    button.SetActive(true);
                }
            }
        }

        private void CheckTraitPoints()
        {
            if (unit.Character.CharacterTraits.Points <= 0)
            {
                AddTraitButton.SetActive(false);
            }
            else
            {
                AddTraitButton.SetActive(true);
            }
        }

        private void FillEquipment()
        {
            if (unit.Inventory.Weapon != null)
            {
                var go = Instantiate(WeaponCanvasPrefab, inventoryCanvas.transform);
                var weapon = go.GetComponent<Weapon>();
                weapon.Setup(unit.Inventory.Weapon.WeaponType, inventoryCanvas);
                WeaponSlot.PutItem(weapon);
            }
            if (unit.Inventory.ArmorType != null)
            {
                var go = Instantiate(ArmorCanvasPrefab, inventoryCanvas.transform);
                var armor = go.GetComponent<Armor>();
                armor.Setup(unit.Inventory.ArmorType, inventoryCanvas);
                ArmorSlot.PutItem(armor);
            }
            if (unit.Inventory.Helmet != null)
            {
                var go = Instantiate(HelmetCanvasPrefab, inventoryCanvas.transform);
                var helmet = go.GetComponent<Helmet>();
                helmet.Setup(unit.Inventory.Helmet.HelmetType, inventoryCanvas);
                HelmetSlot.PutItem(helmet);
            }
            if (unit.Inventory.Vest != null)
            {
                var go = Instantiate(VestCanvasPrefab, inventoryCanvas.transform);
                var vest = go.GetComponent<Vest>();
                vest.Setup(unit.Inventory.Vest.VestType, inventoryCanvas);
                VestSlot.PutItem(vest);
            }
        }

        private void FillInventory()
        {
            foreach (var item in player.PlayerInventory)
            {
                if (item is WeaponType)
                {
                    var go = Instantiate(WeaponCanvasPrefab, inventoryCanvas.transform);
                    go.GetComponent<Weapon>().Setup(item, inventoryCanvas);
                }
                else if(item is ArmorType)
                {
                    var go = Instantiate(ArmorCanvasPrefab, inventoryCanvas.transform);
                    go.GetComponent<Armor>().Setup(item, inventoryCanvas);
                }
                else if (item is HelmetType)
                {
                    var go = Instantiate(HelmetCanvasPrefab, inventoryCanvas.transform);
                    go.GetComponent<Helmet>().Setup(item, inventoryCanvas);
                }
                else if (item is VestType)
                {
                    var go = Instantiate(VestCanvasPrefab, inventoryCanvas.transform);
                    go.GetComponent<Vest>().Setup(item, inventoryCanvas);
                }
            }
        }

        private void AssignPrivates()
        {
            traits = new HashSet<Text>();
            var transforms = GetComponentsInChildren<Transform>();
            foreach (var transform in transforms)
            {
                switch (transform.name)
                {
                    case "InventoryPanel":
                        if (Equals(inventoryCanvas = transform.GetComponent<Canvas>(), null))
                        {
                            inventoryCanvas = transform.gameObject.AddComponent<Canvas>();
                        }
                        break;
                    case "CharacterID_firstName":
                        if (Equals(firstName = transform.GetComponent<Text>(), null))
                        {
                            firstName = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_surname":
                        if (Equals(surname = transform.GetComponent<Text>(), null))
                        {
                            surname = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_nickname":
                        if (Equals(nickname = transform.GetComponent<Text>(), null))
                        {
                            nickname = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_age":
                        if (Equals(age = transform.GetComponent<Text>(), null))
                        {
                            age = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_gender":
                        if (Equals(gender = transform.GetComponent<Text>(), null))
                        {
                            gender = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_affiliation":
                        if (Equals(affiliation = transform.GetComponent<Text>(), null))
                        {
                            affiliation = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_stamina":
                        if (Equals(stamina = transform.GetComponent<Text>(), null))
                        {
                            stamina = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_endurance":
                        if (Equals(endurance = transform.GetComponent<Text>(), null))
                        {
                            endurance = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_marksmanship":
                        if (Equals(marksmanship = transform.GetComponent<Text>(), null))
                        {
                            marksmanship = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_cunning":
                        if (Equals(cunning = transform.GetComponent<Text>(), null))
                        {
                            cunning = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "CharacterID_charisma":
                        if (Equals(charisma = transform.GetComponent<Text>(), null))
                        {
                            charisma = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "Level":
                        if (Equals(level = transform.GetComponent<Text>(), null))
                        {
                            level = transform.gameObject.AddComponent<Text>();
                        }
                        break;
                    case "Trait1":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait2":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait3":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait4":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait5":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait6":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait7":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "Trait8":
                        traits.Add(transform.gameObject.GetComponent<Text>());
                        break;
                    case "CharacterID_flag":
                        if (Equals(flag = transform.GetComponent<RawImage>(), null))
                        {
                            flag = transform.gameObject.AddComponent<RawImage>();
                        }
                        break;
                    case "CharacterID_portrait":
                        if (Equals(portrait = transform.GetComponent<RawImage>(), null))
                        {
                            portrait = transform.gameObject.AddComponent<RawImage>();
                        }
                        break;
                }
            }
        }
    }
}
