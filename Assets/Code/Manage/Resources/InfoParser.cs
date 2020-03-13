using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Manage.Resources
{
    public class InfoParser : IDisposable
    {
        public InfoParser()
        {
        }

        public List<string> ParseNames(string path)
        {
            var output = new List<string>();
            var doc = new XmlDocument();
            doc.Load(path);
            var root = doc.DocumentElement;
            foreach (XmlNode resource in root.ChildNodes)
            {
                output.Add(resource.Attributes["name"].Value);
            }
            return output;
        }

        public List<string> ParseDescriptions(string path)
        {
            var output = new List<string>();
            var doc = new XmlDocument();
            doc.Load(path);
            var root = doc.DocumentElement;
            foreach (XmlNode resource in root.ChildNodes)
            {
                output.Add(resource.Attributes["description"].Value);
            }
            return output;
        }

        public List<Info> MergeNamesAndDescriptions(List<string> names,List<string> descriptions)
        {
            var output = new List<Info>();
            var descCount = descriptions.Count;
            var random = new Random();
            foreach (var name in names)
            {
                var description = descriptions[random.Next(0, descCount)].Replace("[name]",name);
                output.Add(new Info(name, description));
            }
            return output;
        }

        public void Dispose()
        {
        }
    }
}
