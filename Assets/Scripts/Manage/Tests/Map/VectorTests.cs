using Manage.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Manage.Map.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void DistanceTest()
        {
            var vector1 = new Vector3(0, 0, 0);
            var vector2 = new Vector3(1, 1, 1);
            var vector3 = new Vector3(-1, -1, -1);
            Assert.AreEqual(Vector3.Distance(vector1, vector2), 1.73205080757, 0.001);
            Assert.AreEqual(Vector3.Distance(vector1, vector3), 1.73205080757, 0.001);
            Assert.AreEqual(Vector3.Distance(vector2, vector3), 3.46410161514, 0.001);
        }
    }
}