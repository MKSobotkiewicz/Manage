using Manage.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Manage.Organizations.Tests
{
    [TestFixture]
    class FlagLoaderTests
    {
        [Test]
        public void GetFlag()
        {
            var flag=FlagLoader.LoadFlag("Black");

            Assert.AreEqual(flag.height,256);
            Assert.AreEqual(flag.width, 512);
            for (int i=0;i<512;i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    Assert.AreEqual(flag.GetPixel(i,j), Color.black);
                }
            }
        }
    }
}
