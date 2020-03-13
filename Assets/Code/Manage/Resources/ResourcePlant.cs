using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourcePlant : IBuildable, IProducing
    {
        public ResourcePlantType ResourcePlantType { get; private set; }
        public bool IsBuilt { get; private set; }
        public bool IsTurnedOn { get; private set; }
        public ulong ProductionPercentage { get; private set; }
        public ModifiersSet ModifiersSet { get; private set; }
        public ResourceDepot ResourceDepot { get; private set; }

        private UInt64 CapacityPercentage;
        private UInt64 CapacityPercentagePerTick;

        public ResourcePlant(ResourcePlantType resourcePlantType, ModifiersSet  modifiersSet, ResourceDepot resourceDepot)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourcePlant created.");
            ResourcePlantType = resourcePlantType;
            ResourceDepot = resourceDepot;
            ModifiersSet = modifiersSet;
            IsBuilt = false;
            IsTurnedOn = false;
            ProductionPercentage = 0;
            CapacityPercentage = 0;
            CapacityPercentagePerTick = 0;
        }

        public void Build()
        {
            if (!IsBuilt)
            {
                if (ProductionPercentage <= 0)
                {
                    if (0 == (CapacityPercentage = ResourceDepot.CheckResourceRemovalCapacityPercentage(ResourcePlantType.ResourceBuildCosts,
                                                                                                ModifiersSet.BuildCostModifiers)))
                    {
                        Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Not enough resources for " + ResourcePlantType.Info().Name + " to begin building.");
                        return;
                    }
                    if (ResourceDepot.Remove(ResourcePlantType.ResourceBuildCosts * ((double)CapacityPercentage / 100),
                                                       ModifiersSet.BuildCostModifiers
                                                       ).Equals(null))
                    {
                        Debug.Logger.Log(Debug.Logger.InfoLevel.Error, "Error.");
                        throw new NullReferenceException();
                    }

                    if (Debug.Logger._InfoLevel >= Debug.Logger.InfoLevel.InfoLevel1)
                    {
                        var infoString = ResourcePlantType.Info().Name + " began building";
                        if (ResourcePlantType.ResourceProductionCosts.Count > 0)
                        {
                            foreach (var cost in ResourcePlantType.ResourceBuildCosts * ((double)CapacityPercentage / 100))
                            {
                                infoString += ", " + cost.Value + " of " + cost.ResourceType;
                            }
                            infoString += " removed from " + ResourcePlantType.Info().Name + " resource depo.";
                        }
                        else
                        {
                            infoString += ".";
                        }
                        Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, infoString);
                    }
                }
                var addedProductionPercentage = ModifiersSet.BuildSpeedModifiers.Modify(ResourcePlantType.BuildSpeedPerTick);
                ProductionPercentage += addedProductionPercentage;
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Added " + addedProductionPercentage +
                    "% to ProductionPercentage for a total of " + ProductionPercentage + "%.");
                if (ProductionPercentage >= 100)
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, ResourcePlantType.Info().Name + " has beed built.");
                    IsBuilt = true;
                    TurnOn();
                    ProductionPercentage = 0;

                }
            }
            return;
        }

        public void TurnOn()
        {
            IsTurnedOn = true;
        }

        public bool Produce()
        {
            if (IsBuilt && IsTurnedOn)
            {
                if (ProductionPercentage <= 0)
                {
                    if (0==(CapacityPercentage = ResourceDepot.CheckResourceRemovalCapacityPercentage(ResourcePlantType.ResourceProductionCosts,
                                                                                                      ModifiersSet.ProductionCostModifiers)))
                    {
                        Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Not enough resources for " + ResourcePlantType.Info().Name + " to begin production.");
                        return false;
                    }
                    if (ResourceDepot.Remove(ResourcePlantType.ResourceProductionCosts*((double)CapacityPercentage/100),
                                                       ModifiersSet.ProductionCostModifiers).Equals(null))
                    {
                        Debug.Logger.Log(Debug.Logger.InfoLevel.Error, "Error.");
                        throw new NullReferenceException();
                    }

                    if (Debug.Logger._InfoLevel >= Debug.Logger.InfoLevel.InfoLevel1)
                    {
                        var infoString = ResourcePlantType.Info().Name + " began production";
                        if (ResourcePlantType.ResourceProductionCosts.Count > 0)
                        {
                            foreach (var cost in ResourcePlantType.ResourceProductionCosts * ((double)CapacityPercentage / 100))
                            {
                                infoString += ", " + cost.Value + " of " + cost.ResourceType;
                            }
                            infoString += " removed from " + ResourcePlantType.Info().Name + " resource depo.";
                        }
                        else
                        {
                            infoString += ".";
                        }
                        Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, infoString);
                    }
                }
                
                if (0 == (CapacityPercentagePerTick = ResourceDepot.CheckResourceRemovalCapacityPercentage(ResourcePlantType.ResourceProductionCostsPerTick,
                                                                                                           ModifiersSet.ProductionCostModifiersPerTick)))
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Not enough resources for " + ResourcePlantType.Info().Name + " to continue production.");
                    return false;
                }
                if (ResourceDepot.Remove(ResourcePlantType.ResourceProductionCostsPerTick * ((double)CapacityPercentagePerTick / 100),
                                                       ModifiersSet.ProductionCostModifiersPerTick).Equals( null))
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.Error, "Error.");
                    throw new NullReferenceException();
                }
                var addedProductionPercentage = ModifiersSet.ProductionSpeedModifiers.Modify(ResourcePlantType.ResourceProductionSpeedPerTick)* ((double)CapacityPercentagePerTick / 100);
                ProductionPercentage += (UInt64)addedProductionPercentage;
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Added "+ addedProductionPercentage + 
                    "% to ProductionPercentage for a total of "+ ProductionPercentage + "%.");
                if (ProductionPercentage >= 100)
                {
                    ResourceDepot.Add(ResourcePlantType.ResourceProduction* ((double)CapacityPercentage / 100),
                                               ModifiersSet.ProductionModifiers);

                    if (Debug.Logger._InfoLevel>=Debug.Logger.InfoLevel.InfoLevel1)
                    {
                        var infoString = ResourcePlantType.Info().Name + " ended production";
                        if (ResourcePlantType.ResourceProductionCosts.Count > 0)
                        {
                            foreach (var production in ResourcePlantType.ResourceProduction * ((double)CapacityPercentage / 100))
                            {
                                infoString += ", " + production.Value + " of " + production.ResourceType;
                            }
                            infoString += " added to " + ResourcePlantType.Info().Name + " resource depo.";
                        }
                        else
                        {
                            infoString += ".";
                        }
                        Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, infoString);
                    }
                    ProductionPercentage = 0;
                }
            }
            return true;
        }

        public ResourceDepot ProductionPerTick()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Getting ProductionPerTick in ResourcePlant.");

            var productionPerTick = new ResourceDepot();

            if (!IsBuilt)
            {
                return productionPerTick;
            }
            foreach (var production in ResourcePlantType.ResourceProduction)
            {
                if (CapacityPercentage != 0)
                {
                    productionPerTick.Add(production *
                                          (((double)CapacityPercentage * (((double)ResourcePlantType.ResourceProductionSpeedPerTick) / 100)) / 100),
                                          ModifiersSet.ProductionModifiers);
                }
            }
            return productionPerTick;
        }

        public ResourceDepot CostPerTick()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Getting CostPerTick in ResourcePlant.");

            var costPerTick = new ResourceDepot();
            if (!IsBuilt)
            {
                return costPerTick;
            }
            foreach (var cost in ResourcePlantType.ResourceProductionCosts)
            {
                if (CapacityPercentage != 0)
                {
                    costPerTick.Add(cost *
                                (((double)CapacityPercentage * (((double)ResourcePlantType.ResourceProductionSpeedPerTick) / 100)) / 100),
                                ModifiersSet.ProductionCostModifiers);
                }
            }
            foreach (var cost in ResourcePlantType.ResourceProductionCostsPerTick)
            {
                if (CapacityPercentagePerTick != 0)
                {
                    costPerTick.Add(cost *((double)CapacityPercentagePerTick / 100),
                                ModifiersSet.ProductionCostModifiersPerTick);
                }
            }
            return costPerTick;
        }

        public ResourceDepot MaxCostPerTick()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Getting MaxCostPerTick in ResourcePlant.");

            var costPerTick = new ResourceDepot();
            if (!IsBuilt)
            {
                return costPerTick;
            }
            foreach (var cost in ResourcePlantType.ResourceProductionCosts)
            {
                costPerTick.Add(cost *
                            (((double)ResourcePlantType.ResourceProductionSpeedPerTick) / 100),
                            ModifiersSet.ProductionCostModifiers);
            }
            foreach (var cost in ResourcePlantType.ResourceProductionCostsPerTick)
            {
                costPerTick.Add(cost,
                            ModifiersSet.ProductionCostModifiersPerTick);
            }
            return costPerTick;
        }

        public ResourceDepot Deficit()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Getting Deficit in ResourcePlant.");
            var output= (MaxCostPerTick() - CostPerTick());

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, output[0].Value.ToString());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, output[1].Value.ToString());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, output[2].Value.ToString());
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, output[3].Value.ToString());
            return new ResourceDepot(output);
        }
    }
}
