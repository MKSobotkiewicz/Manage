using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Units
{
    public static class VehicleTypes
    {
        public enum EVehicleType
        {
            None,
            ScoutTank,
            Jeep
        }

        public static VehicleType ToVehicleType(EVehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case EVehicleType.ScoutTank:
                    return ScoutTank;
                case EVehicleType.Jeep:
                    return Jeep;
                default:
                    return null;
            }
        }

        public static readonly VehicleType ScoutTank = new VehicleType(
            10,
            100,
            1000,
            WeaponTypes.Cannon_85mm,
            "Units/MGS"
            );
        public static readonly VehicleType Jeep = new VehicleType(
            20,
            50,
            400,
            WeaponTypes.Gun_13mm,
            "Units/Jeep");
    }
}
