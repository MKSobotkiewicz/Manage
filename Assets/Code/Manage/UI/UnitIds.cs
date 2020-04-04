using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage.Units;
using Manage.Control;
using Manage.Camera;

namespace Manage.UI
{
    public class UnitIds : MonoBehaviour
    {
        public GameObject UnitIdPrefab;
        public List<Unit> Units;
        public Player.Player Player;
        public MouseRaycasting MouseRaycasting;
        public CameraBehaviour CameraBehaviour;

        public void Start()
        {
            foreach (var unit in AllUnitsList.Units)
            {
                if (Equals(unit.Character.Organization, Player.Organization))
                {
                    Units.Add(unit);
                }

            }

            int i = 0;
            foreach (var unit in Units)
            {
                var go = Instantiate(UnitIdPrefab,transform);
                var unitId = go.GetComponent<UnitId>();
                unitId.MouseRaycasting = MouseRaycasting;
                unitId.CameraBehaviour = CameraBehaviour;
                unitId.Generate(unit, Player);
                unitId.GetComponent<RectTransform>().anchoredPosition += new Vector2(i,0);
                i += 110;
            }
        }

        public void Reset()
        {
            Units.Clear();
            var unitIds = GetComponentsInChildren<UnitId>();
            foreach (var unitId in unitIds)
            {
                unitId.Destroy();
            }
            Start();
        }
    }
}