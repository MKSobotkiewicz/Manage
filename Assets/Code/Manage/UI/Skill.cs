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
        public AudioSource AudioSource;
        public Text TimeText;
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
            TimeText.enabled = false;
        }

        public void Update()
        {
            TimeText.text = ((int)skill.Timer).ToString();
            if ((int)skill.Timer <= 0)
            {
                TimeText.enabled = false;
            }
            else
            {
                TimeText.enabled = true;
            }
            if (aiming is false)
            {
                return;
            }
            if (skill.IsReady is false)
            {
                StopAiming();
                return;
            }
            if (Input.GetAxis("Cancel") > 0)
            {
                StopAiming();
                return;
            }
        }

        public void Click()
        {
            if (aiming is false)
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
            if (!skill.IsReady)
            {
                return;
            }
            AudioSource.Play();
            aiming = true;
            skill.StartAiming();
            Outline.material = OutlineRedMaterial;
        }

        public void StopAiming()
        {
            if (skill.IsReady is true)
            {
                AudioSource.Play();
            }
            aiming = false;
            skill.StopAiming();
            Outline.material = OutlineBaseMaterial;
        }
    }
}
