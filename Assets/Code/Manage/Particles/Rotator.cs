using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Particles
{
    public class Rotator : MonoBehaviour
    {
        [Range(-100, 100)]
        public float XRotationSpeed = 0;
        [Range(-100, 100)]
        public float YRotationSpeed = 0;
        [Range(-100, 100)]
        public float ZRotationSpeed = 0;

        public void Update()
        {
            var xRotation = XRotationSpeed * Time.deltaTime;
            var yRotation = YRotationSpeed * Time.deltaTime;
            var zRotation = ZRotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(xRotation, yRotation, zRotation));
        }
    }
}
