using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Manage.Resources
{
    public class ResourceTypesParser:IDisposable
    {
        public ResourceTypesParser()
        {
        }

        public List<ResourceType> Parse(string path)
        {
            var output = new List<ResourceType>();

            var directory = new DirectoryInfo(path);
            
            var doc = new XmlDocument();
            using (var stream = (directory.GetFiles("Resources.xml"))[0].Open(FileMode.Open))
            {
                doc.LoadXml(new StreamReader(stream).ReadToEnd());
            }
            var root = doc.DocumentElement;
            foreach (XmlNode resource in root.ChildNodes)
            {
                var isVolatile = resource.Attributes["isVolatile"].Value == "true" ? true : false;
                var name = resource.Attributes["name"].Value;
                Texture2D icon = new Texture2D(0,0);
                if (directory.GetFiles(name + ".png")!=null)
                {
                    using (var stream = (directory.GetFiles(name + ".png"))[0].Open(FileMode.Open))
                    {
                        var buffer = new byte[stream.Length];
                        var data = stream.Read(buffer,0,(int)stream.Length);
                        icon.LoadImage(buffer);
                    }
                }
                output.Add(new ResourceType(name, isVolatile, icon));
            }
            return output;
        }

        public void Dispose()
        {
        }
    }
}
