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
    public class InfoParserTests
    {
        [Test]
        public void MergeNamesAndDescriptionsTest()
        {
            var names = new List<string> { "name1", "name2" };
            var descriptions = new List<string> { "description [name]" };
            var info1 = new List<Info> {new Info("name1", "description name1"),
                                        new Info("name2", "description name2") };

            using (var parser=new InfoParser())
            {
                var info2 = parser.MergeNamesAndDescriptions(names,descriptions);
                Assert.AreEqual(info1[0].Name, info2[0].Name);
                Assert.AreEqual(info1[0].Description, info2[0].Description);
                Assert.AreEqual(info1[1].Name, info2[1].Name);
                Assert.AreEqual(info1[1].Description, info2[1].Description);
            }
        }
    }
}