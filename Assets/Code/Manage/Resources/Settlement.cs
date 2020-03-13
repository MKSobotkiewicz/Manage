using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class Settlement : Info, IProducing, IBuildable
    {
        public List<ResourcePlant> ResourcePlants { get; private set; }
        public ResourceDepot ResourceDepot { get; private set; }

        public Settlement(string name, string description) :base(name,description)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Settlement with name: >>" + name + "<< and description: >>" + description + "<< created.");
            ResourcePlants = new List<ResourcePlant>();
            ResourceDepot = new ResourceDepot();
        }

        public void Build()
        {
            foreach (var resourcePlant in ResourcePlants)
            {
                resourcePlant.Build();
            }
        }

        public bool Produce()
        {
            foreach (var resourcePlant in ResourcePlants)
            {
                resourcePlant.Produce();
            }
            return true;
        }

        public ResourceDepot ProductionPerTick()
        {
            var productionPerTick = new ResourceDepot();
            foreach (var resourcePlant in ResourcePlants)
            {
                productionPerTick = (productionPerTick + resourcePlant.ProductionPerTick()) as ResourceDepot;
            }
            return productionPerTick;
        }

        public ResourceDepot CostPerTick()
        {
            var costPerTick = new ResourceDepot();
            foreach (var resourcePlant in ResourcePlants)
            {
                costPerTick  = (costPerTick  + resourcePlant.CostPerTick())as ResourceDepot;
            }
            return costPerTick;
        }

        public ResourceDepot MaxCostPerTick()
        {
            var maxCostPerTick = new ResourceDepot();
            foreach (var resourcePlant in ResourcePlants)
            {
                maxCostPerTick = (maxCostPerTick + resourcePlant.MaxCostPerTick()) as ResourceDepot;
            }
            return maxCostPerTick;
        }

        public ResourceDepot Deficit()
        {
            var deficit = new ResourceDepot();
            foreach (var resourcePlant in ResourcePlants)
            {
                deficit = (deficit + resourcePlant.Deficit()) as ResourceDepot;
            }
            return deficit;
        }
    }
}
