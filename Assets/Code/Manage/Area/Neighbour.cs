using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Area
{
    public class Neighbour
    {
        public Area Area { get; private set; }
        public double Distance { get; private set; }

        public Neighbour(Area area, double distance)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Neighbour with Area named " + Area.Name + " and distance "+distance.ToString()+" created.");
            Area = area;
            Distance = distance;
        }
    }
}
