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
    public class ResourceValueTests
    {
        [Test]
        public void AddValueTest()
        {
            var resourceType = new ResourceType("resource", false, null);
            var resourceValue1 = new ResourceValue(resourceType, 25);
            var resourceValue2 = new ResourceValue(resourceType, 25);
            Assert.AreEqual((UInt64)50,(resourceValue1+ resourceValue2).Value);
            Assert.AreEqual((UInt64)25, resourceValue1.Value);
            Assert.AreEqual((UInt64)25, resourceValue2.Value);
        }

        [Test]
        public void AddValueTestWrongResource()
        {
            try
            {
                var rightResourceType = new ResourceType("rightResource", false, null);
                var wrongResourceType = new ResourceType("wrongResource", false, null);
                var resourceValue1 = new ResourceValue(rightResourceType, 25);
                var resourceValue2 = new ResourceValue(wrongResourceType, 25);
                var sum = resourceValue1 + resourceValue2;
            }
            catch (InvalidOperationException)
            {
                Assert.Pass("");
            }
            Assert.Fail("");
        }

        [Test]
        public void SubstractValueTest()
        {
            var resourceType = new ResourceType("resource", false, null);
            var resourceValue1 = new ResourceValue(resourceType, 25);
            var resourceValue2 = new ResourceValue(resourceType, 25);
            Assert.AreEqual((UInt64)0, (resourceValue1 - resourceValue2).Value);
            Assert.AreEqual((UInt64)25, resourceValue1.Value);
            Assert.AreEqual((UInt64)25, resourceValue2.Value);
        }

        [Test]
        public void MultiplyValueTest()
        {
            var resourceType = new ResourceType("resource", false, null);
            var resourceValue = new ResourceValue(resourceType, 25);
            Assert.AreEqual((UInt64)100, (resourceValue *4).Value);
            Assert.AreEqual((UInt64)25, resourceValue.Value);
        }

        [Test]
        public void EqualValueTest()
        {
            var resourceType1 = new ResourceType("resource", false, null);
            var resourceType2 = new ResourceType("resource", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 25);
            var resourceValue2 = new ResourceValue(resourceType2, 25);
            Assert.AreEqual( resourceValue1.ToString(), resourceValue2.ToString());
        }

        [Test]
        public void NotEqualValueTest1()
        {
            var resourceType1 = new ResourceType("resource1", false, null);
            var resourceType2 = new ResourceType("resource2", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 25);
            var resourceValue2 = new ResourceValue(resourceType2, 25);
            Assert.AreNotEqual(resourceValue1, resourceValue2);
        }

        [Test]
        public void NotEqualValueTest2()
        {
            var resourceType1 = new ResourceType("resource", false, null);
            var resourceType2 = new ResourceType("resource", false, null);
            var resourceValue1 = new ResourceValue(resourceType1, 50);
            var resourceValue2 = new ResourceValue(resourceType2, 25);
            Assert.AreNotEqual(resourceValue1, resourceValue2);
        }
    }
}