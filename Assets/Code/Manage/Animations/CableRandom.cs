using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Animations
{
    class CableRandom: MonoBehaviour
    {
        private static readonly System.Random random = new System.Random();

        private float timer = 1;
        private Animator animator;

        public void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += 1;
                SetRandom();
            }
        }

        private void SetRandom()
        {
            animator.SetFloat("Random", (float)(random.NextDouble()*0.4+0.8));
        }
    }
}
