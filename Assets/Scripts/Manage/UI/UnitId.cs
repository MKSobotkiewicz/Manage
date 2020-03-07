using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manage.Units;
using Manage.Control;
using Manage.Camera;

namespace Manage.UI
{
    public class UnitId : MonoBehaviour
    {
        public Text UnitIdNickname;
        public Text UnitIdLife;
        public Text UnitIdAmmo;
        public Text UnitIdX;
        public Image Outline;
        public RawImage Portrait;
        public RawImage Weapon;
        public GameObject CharacterIdPrefab;
        public RectTransform LifePanel;
        public RectTransform WeaponPanel;
        public RectTransform FloatingLifePanel;
        public RectTransform FloatingLifeBar;

        public MouseRaycasting MouseRaycasting;
        public CameraBehaviour CameraBehaviour;

        public Material BaseUiMaterial;
        public Material RedUiMaterial;

        public Unit Unit { get; set; }
        private Player.Player player;

        private int lastHitPoints=0;
        private float lastClick=0;

        public void Generate( Unit _unit,Player.Player _player)
        {
            Unit = _unit;
            player = _player;
            UnitIdX.enabled = false;
            if (Unit.Character.Nickname == "")
            {
                UnitIdNickname.text = Unit.Character.Surname;
            }
            else
            {
                UnitIdNickname.text = Unit.Character.Nickname;
            }
            Portrait.texture = Unit.Character.Portrait;
            if (Unit.Inventory.VehicleType == null)
            {
                Weapon.texture = Unit.Inventory.Weapon.WeaponType.CombatIcon();
            }
            SetHitPoints();
            FloatingLifePanel.SetParent(transform.parent);
        }

        public void Start()
        {
        }

        public void Update()
        {
            lastClick -= Time.fixedDeltaTime;
            if (Unit != null)
            {
                SetHitPoints();
                SetAmmo();
                SetSelected();
                MoveFloatingLifePanel();
            }
        }

        public void OnClick()
        {
            if (lastClick > 0 && MouseRaycasting.SelectedUnits.Contains(Unit))
            {
                CameraBehaviour.SetTarget(Unit.Position());
            }
            MouseRaycasting.beganSelecting = false;
            if (Input.GetAxis("MultiChoose")<=0)
            {
                MouseRaycasting.SelectedUnits.Clear();
            }
            lastClick = 1;
            MouseRaycasting.SelectedUnits.Add(Unit);
        }

        public void MoveFloatingLifePanel()
        {
            FloatingLifePanel.localPosition = UnityEngine.Camera.main.WorldToScreenPoint(Unit.transform.position);
        }

        public void SetHitPoints()
        {
            UnitIdLife.text = Mathf.Clamp(Unit.HitPoints(),0,float.MaxValue).ToString() + "/" + Unit.GetMaxHitPoints().ToString();
            float value = (Mathf.Clamp((float)Unit.HitPoints(), 0, float.MaxValue) / (float)Unit.GetMaxHitPoints()) * 2;
            UnitIdLife.color = new Color(2 - value, value, 0);
            LifePanel.localScale = new Vector3(value / 2, 1, 1);
            FloatingLifeBar.localScale = new Vector3(value / 2, 1, 1);
            if (lastHitPoints> Unit.HitPoints())
            {
                GetDamage();
            }
            lastHitPoints = Unit.HitPoints();
        }

        public void OpenInfo()
        {
            UnityEngine.Debug.Log("OpenInfo");
            var go=Instantiate(CharacterIdPrefab,transform);
            var characterId = go.GetComponent<CharacterId>();
            if (Unit.Character == null)
            {
                return;
            }
            characterId.FillFields(Unit, player,this);
            characterId.transform.SetParent( transform.parent);
        }

        private void SetAmmo()
        {
            if (Unit.Inventory.VehicleType == null)
            {
                UnitIdAmmo.text = Unit.Inventory.Weapon.Ammo.ToString() + "/" + Unit.Inventory.Weapon.WeaponType.Ammo.ToString();
                float value = (Mathf.Clamp((float)Unit.Inventory.Weapon.Ammo, 0, float.MaxValue) / (float)Unit.Inventory.Weapon.WeaponType.Ammo) * 2;
                UnitIdAmmo.color = new Color(2 - value, value, 0);
                WeaponPanel.localScale = new Vector3(value / 2, 1, 1);
            }
        }

        private void SetSelected()
        {
            if (MouseRaycasting.SelectedUnits.Contains(Unit))
            {
                Outline.material = RedUiMaterial;
                return;
            }
            Outline.material = BaseUiMaterial;
        }

        private void GetDamage()
        {
            if (Unit.HitPoints() <= 0)
            {
                UnitIdX.enabled=true;
                FloatingLifePanel.gameObject.SetActive(false);
            }
            else
            {
                UnitIdX.enabled = false;
            }
        }
    }
}