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
    public class ResourceValueListTests
    {
        [Test]
        public void MultiplyTest()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 400);
            var resourceValue3 = new ResourceValue(resourceType1, 20);
            var resourceValue4 = new ResourceValue(resourceType2, 80);
            var resourceValueList1 = new ResourceValueList { resourceValue1, resourceValue2 };
            var resourceValueList2 = new ResourceValueList { resourceValue3, resourceValue4 };
            var resourceValueList3 = new ResourceValueList { resourceValue1, resourceValue2 };

            Assert.AreEqual ((resourceValueList1*0.2d).ToString(), resourceValueList2.ToString());
            Assert.AreEqual( resourceValueList1.ToString(), resourceValueList3.ToString());
        }

        [Test]
        public void AddTest()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 100);
            var resourceValue2 = new ResourceValue(resourceType2, 400);
            var resourceValue3 = new ResourceValue(resourceType1, 20);
            var resourceValue4 = new ResourceValue(resourceType2, 80);
            var resourceValue5 = new ResourceValue(resourceType1, 120);
            var resourceValue6 = new ResourceValue(resourceType2, 480);
            var resourceValueList1 = new ResourceValueList { resourceValue1, resourceValue2 };
            var resourceValueList2 = new ResourceValueList { resourceValue3, resourceValue4 };
            var resourceValueList3 = new ResourceValueList { resourceValue5, resourceValue6 };

            Assert.AreEqual((resourceValueList1 + resourceValueList2).ToString(), resourceValueList3.ToString());
        }
    }
}