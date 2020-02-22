using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Units
{
    public static class VehicleTypes
    {
        public static readonly Vehicle MGS = new Vehicle(
            10,
            100,
            1000,
            WeaponTypes.Cannon_85mm,
            "Units/MGS"
            );
        public static readonly Vehicle Jeep = new Vehicle(
            20,
            50,
            400,
            WeaponTypes.Gun_13mm,
            "Units/Jeep");
    }
}
