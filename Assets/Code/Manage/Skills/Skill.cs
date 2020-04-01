﻿using System;
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
        public Texture2D Icon { get; protected set; }
        public GameObject ProjectorPrefab { get; protected set; }

        protected int reloadTime = 0;

        private float timer = 0;
        private bool isAiming=false;
        private GameObject aimingProjector;

        public void Update()
        {
            if (IsReady is false)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
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

        public void EndAiming()
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
            uint cunning = 0;
            foreach(var unit in playerUnits)
            {
                cunning += unit.Cunning();
            }
            timer = reloadTime* (float)Math.Pow(0.9, cunning);
            IsReady = false;
        }
    }
}
