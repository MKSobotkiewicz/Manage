using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Manage.Units;

namespace Manage.Skills
{
    public class Skill : MonoBehaviour
    {
        public bool IsReady = true;
        public Player.Player Player;
        public Texture2D Icon { get; protected set; }
        public GameObject ProjectorPrefab { get; protected set; }

        public float Timer { get; protected set; } = 0;

        protected int reloadTime = 0;

        private bool isAiming=false;
        private GameObject aimingProjector;
        private Vector3 position=new Vector3();

        public void Update()
        {
            if (IsReady is false)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer <= 0)
            {
                IsReady = true;
            }
            if (isAiming)
            {
                var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 200))
                {
                    aimingProjector.transform.position = hit.point;
                    position = hit.point;
                }
                if (Input.GetAxis("Select") > 0)
                {
                    Use(Player.Units);
                }
            }
        }

        public void StartAiming()
        {
            if (isAiming is true)
            {
                return;
            }
            isAiming = true;
            aimingProjector = Instantiate(ProjectorPrefab, transform);
        }

        public void StopAiming()
        {
            if (isAiming is false)
            {
                return;
            }
            isAiming = false;
            Destroy(aimingProjector);
        }

        public void Use(List<Unit> playerUnits)
        {
            if (isAiming is true)
            {
                StopAiming();
            }
            uint cunning = 0;
            foreach(var unit in playerUnits)
            {
                cunning += unit.Cunning();
            }
            Timer = reloadTime* (float)Math.Pow(0.95, cunning);
            IsReady = false;
            if (this is HealingAura)
            {
                ((HealingAura)this).Use(position, playerUnits);
            } else if (this is ArtilleryStrike)
            {
                ((ArtilleryStrike)this).Use(position);
            }
        }
    }
}
