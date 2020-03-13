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
    public class ResourceTypeTests
    {
        [Test]
        public void EqualsTest()
        {
            Assert.AreEqual(new ResourceType ("resourceType", false, null), new ResourceType("resourceType", false, null));
            Assert.IsTrue(new ResourceType("resourceType", false, null).Equals(new ResourceType("resourceType", false, null)));
        }

        [Test]
        public void NotEqualsTest()
        {
            Assert.AreNotEqual(new ResourceType("resourceType1", false, null), new ResourceType("resourceType2", false, null));
            Assert.IsFalse(new ResourceType("resourceType1", false, null).Equals(new ResourceType("resourceType2", false, null)));
        }
    }
}