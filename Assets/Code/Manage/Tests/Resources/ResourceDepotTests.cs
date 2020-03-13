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
    public class ResourceDepotTests
    {
        [Test]
        public void AddResourcesTest()
        {
            var resourceType1 = new ResourceType("resource1",false,null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 200);
            var resourceDepot = new ResourceDepot();

            resourceDepot.Add(resourceValue1).Add(resourceValue2);

            Assert.AreEqual(resourceValue1.ToString(), resourceDepot.Find(resourceType1).ToString());
            Assert.AreEqual(resourceValue2.ToString(), resourceDepot.Find(resourceType2).ToString());

            resourceDepot.Add(resourceValue2);

            Assert.AreEqual(new ResourceValue(resourceType2, 400).Value.ToString(), resourceDepot.Find(resourceType2).Value.ToString());
        }

        [Test]
        public void SubstractResourcesTest1()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 400);
            var resourceDepot = new ResourceDepot();

            resourceDepot.Add(resourceValue1).Add(resourceValue2);

            Assert.AreEqual(resourceValue1.ToString(), resourceDepot.Find(resourceType1).ToString());
            Assert.AreEqual(resourceValue2.ToString(), resourceDepot.Find(resourceType2).ToString());

            resourceDepot.RemoveResources(new ResourceValue(resourceType2, 100));

            Assert.AreEqual(new ResourceValue(resourceType2, 300).Value.ToString(), resourceDepot.Find(resourceType2).Value.ToString());
        }

        [Test]
        public void SubstractResourcesTest2()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 200);
            var resourceDepot = new ResourceDepot();

            Assert.IsNull(resourceDepot.RemoveResources(new ResourceValue(resourceType2, 100)));

            resourceDepot.Add(resourceValue1).Add(resourceValue2);

            Assert.AreEqual(resourceValue1.ToString(), resourceDepot.Find(resourceType1).ToString());
            Assert.AreEqual(resourceValue2.ToString(), resourceDepot.Find(resourceType2).ToString());

            Assert.IsNull(resourceDepot.RemoveResources(new ResourceValue(resourceType2, 500)));
        }

        [Test]
        public void CheckResourceRemoveCapacityPercentageTest()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 200);
            var resourceValue3 = new ResourceValue(resourceType1, 200);
            var resourceDepot = new ResourceDepot();

            resourceDepot.Add(resourceValue1).Add(resourceValue2);

            Assert.AreEqual((UInt64)50, resourceDepot.CheckResourceRemovalCapacityPercentage(resourceValue3));
            Assert.AreEqual((UInt64)100, resourceDepot.CheckResourceRemovalCapacityPercentage(resourceValue2));
            Assert.AreEqual((UInt64)50, resourceDepot.CheckResourceRemovalCapacityPercentage(new ResourceValueList { resourceValue3 , resourceValue2 }));
        }
    }
}