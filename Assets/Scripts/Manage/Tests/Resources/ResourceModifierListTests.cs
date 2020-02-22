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
    public class ResourceModifierListTests
    {
        [Test]
        public void ResourceModifierListTestSubstract()
        {
            var resourceType = new ResourceType ("resource", false, null);
            var resourceModifierList = new ResourceModifierList { new ResourceModifier (resourceType, -0.1d)  };
            Assert.AreEqual(new ResourceValue(resourceType,90).ToString(), 
                            resourceModifierList.Modify(new ResourceValue(resourceType, 100)).ToString());
        }

        [Test]
        public void ResourceModifierListTestAdd()
        {
            var resourceType = new ResourceType("resource", false, null);
            var resourceModifierList = new ResourceModifierList { new ResourceModifier(resourceType, 0.1d) };
            Assert.AreEqual(new ResourceValue(resourceType, 110).ToString(),
                            resourceModifierList.Modify(new ResourceValue(resourceType, 100)).ToString());
        }

        [Test]
        public void ResourceModifierListTestSubstractAndAdd()
        {
            var resourceType = new ResourceType("resource", false, null);
            var resourceModifierList = new ResourceModifierList { new ResourceModifier(resourceType, -0.1d),
                                                                  new ResourceModifier(resourceType, 0.1d)};
            Assert.AreEqual(new ResourceValue(resourceType, 100).ToString(),
                            resourceModifierList.Modify(new ResourceValue(resourceType, 100)).ToString());
        }

        [Test]
        public void ResourceModifierListTestMultipleResources()
        {
            var rightResourceType = new ResourceType("rightResource", false, null);
            var wrongResourceType = new ResourceType("wrongResource", false, null);
            var resourceModifierList = new ResourceModifierList { new ResourceModifier(rightResourceType, -0.1d),
                                                                  new ResourceModifier(wrongResourceType, 0.1d)};
            Assert.AreEqual(new ResourceValue(rightResourceType, 90).ToString(),
                            resourceModifierList.Modify(new ResourceValue(rightResourceType, 100)).ToString());
        }
    }
}