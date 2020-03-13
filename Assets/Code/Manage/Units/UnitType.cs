using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Units
{
    public class UnitType
    {
        public float Mass { get; private set; }
        public float Size { get; private set; }
        public float Speed { get; private set; }
        public int HitPoints { get; private set; }

        public UnitType(float mass,float size,float speed, int hitPoints)
        {
            HitPoints = hitPoints;
            Mass = mass;
            Size = size;
            Speed = speed;
        }
    }
}
