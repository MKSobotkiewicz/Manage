using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class Info : IEquatable<Info>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Info(string name,string description)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Info with name: >>" + name + "<< and description: >>" + description + "<< created.");
            Name = name;
            Description = description;
        }

        public static bool operator ==(Info info1,Info info2)
        {
            return (info1.Name==info2.Name &&
                    info1.Description==info2.Description);
        }

        public static bool operator !=(Info info1,Info info2)
        {
            return !(info1==info2);
        }

        public bool Equals(Info info)
        {
            return this==info;
        }

        public override bool Equals(Object obj)
        {
            return Equals(obj as Info);
        }

        public override int GetHashCode()
        {
            return (this as Object).GetHashCode();
        }
    }
}
