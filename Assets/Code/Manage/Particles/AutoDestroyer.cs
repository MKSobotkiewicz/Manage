using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Particles
{
    public class AutoDestroyer : MonoBehaviour
    {
        public float lifetime=0;

        public void Start()
        {
        }

        public void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}