using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Area
{
    public class Area
    {
        public List<Neighbour> Neighbours { get; private set; }
        public string Name { get; private set; }

        public Area(string name)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2,"Object of type Area named "+name+" created.");
            Name = name;
            
        }

        public void SetNeighbours(List<Neighbour> neighbours)
        {
            Neighbours = neighbours;
        }
    }
}
