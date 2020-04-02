using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manage.Space
{
    public static class Random
    {
        private static System.Random random =new System.Random();

        public static Vector3 PointInSphere(Vector3 center, float radius)
        {
            float x, y, z, d;
            do
            {
                x = (float)(random.NextDouble() * 2 - 1);
                y = (float)(random.NextDouble() * 2 - 1);
                z = (float)(random.NextDouble() * 2 - 1);
                d = x * x + y * y + z * z;
            }
            while (d > 1);
            return center+new Vector3(x* radius, y* radius, z* radius);
        }
    }
}
