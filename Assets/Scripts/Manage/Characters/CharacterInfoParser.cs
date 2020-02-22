using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Manage.Characters
{
    public class CharacterInfoParser : IDisposable
    {
        public CharacterInfoParser()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Created new CharacterInfoParser");
        }

        public List<string> ParseNames(string path)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "CharacterInfoParser parses document: "+path);
            var output = new List<string>();
            var doc = new XmlDocument();
            doc.Load(path);
            var root = doc.DocumentElement;
            foreach (XmlNode resource in root.ChildNodes)
            {
                output.Add(resource.Attributes["name"].Value);
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Parsed name: " + output[output.Count-1]);
            }
            return output;
        }

        public List<Texture2D> ParseIcons(string path)
        {
            var output = new List<Texture2D>();

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles("*.jpg");
            var names = new List<string>();
            foreach (var file in files)
            {
                names.Add(path.Replace("Assets/Resources/","")+"/" + file.Name.Replace(".jpg", ""));
            }
            foreach (var name in names)
            {
                output.Add(UnityEngine.Resources.Load<Texture2D>(name));
                if (Equals(output[output.Count-1], null))
                {
                    throw new Exception();
                }
            }
            return output;
        }

        public void Dispose()
        {
        }
    }
}
