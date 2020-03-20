using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Particles
{
    public class Rotator:MonoBehaviour
    {
        public void Start()
        {
            RotateToNormal();
        }

        public void RotateToNormal()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
            {
                transform.rotation = Quaternion.LookRotation(transform.forward, hit.normal);
            }
        }
    }
}
