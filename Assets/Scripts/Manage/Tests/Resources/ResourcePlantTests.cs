using Manage.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Manage.Resources.Tests
{
    [TestFixture]
    public class ResourcePlantTests
    {
        [Test]
        public void ProduceTest1()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource = new ResourceType("Production Cost Resource", false, null);
            var producedResource = new ResourceType("Produced Resource", false, null);
            var productionCost = new ResourceValue(productionCostResource, 100);
            var produced = new ResourceValue(producedResource, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { produced },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(productionCost);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource).ToString(), new ResourceValue(producedResource, 100).ToString());
            Assert.AreEqual(resourceDepot.Find(productionCostResource), null);
        }

        [Test]
        public void ProduceTest2()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource = new ResourceType("Production Cost Resource", false, null);
            var producedResource = new ResourceType("Produced Resource", false, null);
            var productionCost = new ResourceValue(productionCostResource, 100);
            var produced = new ResourceValue(producedResource, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { produced },
                                                          new ResourceValueList { },
                                                          50,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(productionCost);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource), null);
            Assert.AreEqual(resourceDepot.Find(productionCostResource), null);

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource).ToString(), new ResourceValue(producedResource, 100).ToString());
            Assert.AreEqual(resourceDepot.Find(productionCostResource), null);
        }

        [Test]
        public void ProduceTest3()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource = new ResourceType("Production Cost Resource", false, null);
            var producedResource = new ResourceType("Produced Resource", false, null);
            var productionCost = new ResourceValue(productionCostResource, 100);
            var produced = new ResourceValue(producedResource, 200);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { produced },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(productionCost * 3);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource).ToString(), new ResourceValue(producedResource, 400).ToString());
            Assert.AreEqual(resourceDepot.Find(productionCostResource).ToString(), new ResourceValue(productionCostResource, 100).ToString());
        }

        [Test]
        public void ProduceTest4()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource1 = new ResourceType("Production Cost Resource1", false, null);
            var productionCostResource2 = new ResourceType("Production Cost Resource2", false, null);
            var producedResource1 = new ResourceType("Produced Resource1", false, null);
            var producedResource2 = new ResourceType("Produced Resource2", false, null);
            var productionCost1 = new ResourceValue(productionCostResource1, 100);
            var productionCost2 = new ResourceValue(productionCostResource2, 200);
            var produced1 = new ResourceValue(producedResource1, 300);
            var produced2 = new ResourceValue(producedResource2, 400);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost1, productionCost2 },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { produced1, produced2 },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(productionCost1 * 3).Add(productionCost2 * 3);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource1).ToString(), new ResourceValue(producedResource1, 600).ToString());
            Assert.AreEqual(resourceDepot.Find(producedResource2).ToString(), new ResourceValue(producedResource2, 800).ToString());
            Assert.AreEqual(resourceDepot.Find(productionCostResource1).ToString(), new ResourceValue(productionCostResource1, 100).ToString());
            Assert.AreEqual(resourceDepot.Find(productionCostResource2).ToString(), new ResourceValue(productionCostResource2, 200).ToString());
        }

        [Test]
        public void ProduceTest5()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type1Value = new ResourceValue(type1, 100);
            var type2Value = new ResourceValue(type2, 100);
            var type3Value = new ResourceValue(type3, 100);
            var resourcePlantType1to2 = new ResourcePlantType(new Info("Resource Plant", ""),
                                                              new ResourceValueList { type1Value },
                                                              new ResourceValueList { },
                                                              new ResourceValueList { type2Value },
                                                              new ResourceValueList { },
                                                              100,
                                                              100);
            var resourcePlantType2to3 = new ResourcePlantType(new Info("Resource Plant", ""),
                                                              new ResourceValueList { type2Value },
                                                              new ResourceValueList { },
                                                              new ResourceValueList { type3Value },
                                                              new ResourceValueList { },
                                                              100,
                                                              100);
            var resourcePlant1to2 = new ResourcePlant(resourcePlantType1to2, modifiersSet, resourceDepot);
            var resourcePlant2to3 = new ResourcePlant(resourcePlantType2to3, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(type1Value);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant1to2.Build();
            resourcePlant2to3.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant1to2.Produce();
            resourcePlant2to3.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(type1), null);
            Assert.AreEqual(resourceDepot.Find(type2), null);
            Assert.AreEqual(resourceDepot.Find(type3).ToString(), new ResourceValue(type3, 100).ToString());
        }

        [Test]
        public void ProduceTest6()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource = new ResourceType("Production Cost Resource", false, null);
            var producedResource = new ResourceType("Produced Resource", false, null);
            var productionCost = new ResourceValue(productionCostResource, 100);
            var produced = new ResourceValue(producedResource, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { produced },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValue(productionCostResource, 50));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(producedResource).Value, new ResourceValue(producedResource, 50).Value);
            Assert.AreEqual(resourceDepot.Find(productionCostResource), null);
        }

        [Test]
        public void ProduceTest7()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var productionCostResource = new ResourceType("Production Cost Resource", false, null);
            var productionCostResourcePerTick = new ResourceType("Production Cost Resource per Tick", false, null);
            var productionCost = new ResourceValue(productionCostResource, 100);
            var productionCostPerTick = new ResourceValue(productionCostResourcePerTick, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { productionCost },
                                                          new ResourceValueList { productionCostPerTick },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValue(productionCostResource, 100)).Add( new ResourceValue(productionCostResourcePerTick, 50));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourceDepot.Find(productionCostResource), null);
            Assert.AreEqual(resourceDepot.Find(productionCostResourcePerTick), null);
        }

        [Test]
        public void ProductionPerTickTest1()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type1Value = new ResourceValue(type1, 100);
            var type2Value = new ResourceValue(type2, 200);
            var type3Value = new ResourceValue(type3, 300);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { type1Value, type2Value, type3Value },
                                                          new ResourceValueList { },
                                                          10,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Assert.AreEqual(resourcePlant.ProductionPerTick().ToString(), new ResourceDepot { new ResourceValue(type1, 10),
                                                                                              new ResourceValue(type2, 20),
                                                                                              new ResourceValue(type3, 30)}.ToString());
        }

        [Test]
        public void ProductionPerTickTest2()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type4 = new ResourceType("Type 4", false, null);
            var type1Value = new ResourceValue(type1, 100);
            var type2Value = new ResourceValue(type2, 200);
            var type3Value = new ResourceValue(type3, 300);
            var type4Value = new ResourceValue(type4, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { type4Value },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { type1Value, type2Value, type3Value },
                                                          new ResourceValueList { },
                                                          10,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValue(type4, 50));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Assert.AreEqual(resourcePlant.ProductionPerTick().ToString(), new ResourceDepot { new ResourceValue(type1, 5),
                                                                                              new ResourceValue(type2, 10),
                                                                                              new ResourceValue(type3, 15)}.ToString());
        }

        [Test]
        public void ProductionPerTickTest3()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type4 = new ResourceType("Type 4", false, null);
            var type1Value = new ResourceValue(type1, 100);
            var type2Value = new ResourceValue(type2, 200);
            var type3Value = new ResourceValue(type3, 300);
            var type4Value = new ResourceValue(type4, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { type4Value },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { type1Value, type2Value, type3Value },
                                                          new ResourceValueList { },
                                                          10,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValue(type4, 50));
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Assert.AreEqual(resourcePlant.ProductionPerTick().ToString(), new ResourceDepot { }.ToString());
        }

        [Test]
        public void CostPerTickTest1()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type4 = new ResourceType("Type 4", false, null);
            var type1Value = new ResourceValue(type1, 100);
            var type2Value = new ResourceValue(type2, 200);
            var type3Value = new ResourceValue(type3, 300);
            var type4Value = new ResourceValue(type4, 100);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { type1Value, type2Value, type3Value },
                                                          new ResourceValueList { type4Value },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { },
                                                          10,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValueList { type1Value,type2Value,type3Value,new ResourceValue(type4, 50) });
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");
            
            Assert.AreEqual(resourcePlant.CostPerTick().ToString(), new ResourceDepot { new ResourceValue(type1, 10),
                                                                                        new ResourceValue(type2, 20),
                                                                                        new ResourceValue(type3, 30),
                                                                                        new ResourceValue(type4, 50)}.ToString());

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");

            Assert.AreEqual(resourcePlant.CostPerTick().ToString(), new ResourceDepot { new ResourceValue(type1, 10),
                                                                                        new ResourceValue(type2, 20),
                                                                                        new ResourceValue(type3, 30)}.ToString());

            Assert.AreEqual(resourcePlant.MaxCostPerTick().ToString(), new ResourceDepot { new ResourceValue(type1, 10),
                                                                                           new ResourceValue(type2, 20),
                                                                                           new ResourceValue(type3, 30),
                                                                                           new ResourceValue(type4, 100)}.ToString());
        }

        [Test]
        public void CostPerTickTest2()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "Start of definitions");
            var resourceDepot = new ResourceDepot();
            var modifiersSet = new ModifiersSet();
            var type1 = new ResourceType("Type 1", false, null);
            var type2 = new ResourceType("Type 2", false, null);
            var type3 = new ResourceType("Type 3", false, null);
            var type4 = new ResourceType("Type 4", false, null);
            var type1Value = new ResourceValue(type1, 10);
            var type2Value = new ResourceValue(type2, 20);
            var type3Value = new ResourceValue(type3, 30);
            var type4Value = new ResourceValue(type4, 10);
            var resourcePlantType = new ResourcePlantType(new Info("Resource Plant", ""),
                                                          new ResourceValueList { type1Value, type2Value, type3Value },
                                                          new ResourceValueList { type4Value },
                                                          new ResourceValueList { },
                                                          new ResourceValueList { },
                                                          100,
                                                          100);
            var resourcePlant = new ResourcePlant(resourcePlantType, modifiersSet, resourceDepot);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "End of definitions");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) start");
            resourceDepot.Add(new ResourceValueList { new ResourceValue(type1, 5), type2Value, new ResourceValue(type1, 15), type4Value });
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourceDepot.AddResources(productionCost) end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() start");
            resourcePlant.Build();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Build() end");

            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() start");
            resourcePlant.Produce();
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, "resourcePlant.Produce() end");
            
            Assert.AreEqual(resourcePlant.CostPerTick().ToString(), new ResourceValueList { }.ToString());

            Assert.AreEqual(resourcePlant.MaxCostPerTick().ToString(), new ResourceValueList { new ResourceValue(type1, 10),
                                                                                               new ResourceValue(type2, 20),
                                                                                               new ResourceValue(type3, 30),
                                                                                               new ResourceValue(type4, 10)}.ToString());

            Assert.AreEqual(resourcePlant.Deficit().ToString(), new ResourceValueList { new ResourceValue(type1, 10),
                                                                                        new ResourceValue(type2, 20),
                                                                                        new ResourceValue(type3, 30),
                                                                                        new ResourceValue(type4, 10)}.ToString());
        }
    }
}