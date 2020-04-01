using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class Skill : MonoBehaviour
    {
        public RawImage Icon;
        public Image Outline;
        public Material OutlineBaseMaterial;
        public Material OutlineRedMaterial;

        private Manage.Skills.Skill skill;

        private bool aiming = false;

        public void Load(Manage.Skills.Skill _skill)
        {
            skill = _skill;
            UnityEngine.Debug.Log(skill.Icon);
            Icon.texture = skill.Icon;
            Outline.material = OutlineBaseMaterial;
        }

        public void Click()
        {
            if (!(aiming is true))
            {
                foreach (var skill in transform.GetComponentInParent<UI.Skills>().SkillsList)
                {
                    if (skill.aiming is true)
                    {
                        skill.StopAiming();
                    }
                }
                StartAiming();
                return;
            }
            StopAiming();
        }

        public void StartAiming()
        {
            aiming = true;
            skill.StartAiming();
            Outline.material = OutlineRedMaterial;
        }

        public void StopAiming()
        {
            aiming = false;
            skill.EndAiming();
            Outline.material = OutlineBaseMaterial;
        }
    }
}
