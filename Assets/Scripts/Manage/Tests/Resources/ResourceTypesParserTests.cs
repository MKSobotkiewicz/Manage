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
    public class ResourceTypesParserTests
    {
        [Test]
        public void ResourceTypesParserTest()
        {
            using (var resourceTypesParser = new ResourceTypesParser())
            {
                var testResource1Icon = new Texture2D(16, 16);
                var testResource2Icon = new Texture2D(16, 16);
                for (int i=0;i<16; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        testResource1Icon.SetPixel(i, j, Color.white);
                        testResource2Icon.SetPixel(i, j, Color.black);
                    }
                }
                var path = "Assets\\Scripts\\Manage\\Assets\\Resources\\TestResources";
                var resurceTypes = resourceTypesParser.Parse(path);
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1, 
                                 "resource1: name: "+ resurceTypes[0].Name + " isVolatile: "+ resurceTypes[0].Volatile);
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel1,
                                 "resource2: name: " + resurceTypes[1].Name + " isVolatile: " + resurceTypes[1].Volatile);
                Assert.AreEqual(resurceTypes[0], new ResourceType("testResource1", true, testResource1Icon));
                Assert.AreEqual(resurceTypes[1], new ResourceType("testResource2", false, testResource2Icon));
            }
        }
    }
}