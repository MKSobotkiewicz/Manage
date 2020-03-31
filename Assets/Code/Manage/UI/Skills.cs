using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Manage.UI
{
    public class Skills : MonoBehaviour
    {
        public Player.Player Player;

        public Canvas SkillsCanvas;
        public Skill SkillPrefab;

        public void Start()
        {
            foreach (var skill in Player.PlayerSkills)
            {
                var go = Instantiate(SkillPrefab, SkillsCanvas.transform);
                go.Load(skill);
            }
            Resize();
        }

        public void Resize()
        {
            var rt = GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.sizeDelta = new Vector2(rt.sizeDelta.x,
                                           10 + Player.PlayerSkills.Count * (10 + SkillPrefab.GetComponent<RectTransform>().sizeDelta.y));
            }
        }
    }
}
