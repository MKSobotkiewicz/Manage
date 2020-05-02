using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Utilities
{
    public class Sign:MonoBehaviour
    {
        public MeshRenderer MeshRenderer;
        public Light Light;

        private float timer=0;
        private bool isEmissive=false;

        private static readonly System.Random random = new System.Random();

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = (float)(random.NextDouble()+1);
                if (isEmissive)
                {
                    MeshRenderer.material.SetFloat("_isEmissive",0);
                    Light.gameObject.SetActive(false);
                    isEmissive = false;
                }
                else
                {
                    MeshRenderer.material.SetFloat("_isEmissive", 1);
                    Light.gameObject.SetActive(true);
                    isEmissive = true;
                }
            }
        }
    }
}
