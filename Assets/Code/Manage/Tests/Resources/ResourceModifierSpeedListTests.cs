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
    public class ResourceModifierSpeedListTests
    {
        [Test]
        public void ResourceModifierSpeedListTest_Substract()
        {
            Assert.AreEqual((ulong)90, new ResourceModifierSpeedList { -0.1d }.Modify(100));
        }

        [Test]
        public void ResourceModifierSpeedListTest_Add()
        {
            Assert.AreEqual((ulong)110, new ResourceModifierSpeedList { 0.1d }.Modify(100));
        }

        [Test]
        public void ResourceModifierSpeedListTest_SubstractAndAdd()
        {
            Assert.AreEqual((ulong)100, new ResourceModifierSpeedList { -0.1d, 0.1d }.Modify(100));
        }
    }
}