using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Resources
{
    public class ResourceType : IEquatable<ResourceType>
    {
        public string Name { get; private set; }
        public Texture2D Icon { get; private set; }
        public bool Volatile { get; private set; }

        public ResourceType(string name,bool isVolatile, Texture2D icon)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourceType named " + name + " created.");
            Name = name;
            Icon = icon;
            Volatile = isVolatile;
        }

        public static bool operator==(ResourceType resourceType1, ResourceType resourceType2)
        {
            if (resourceType1.Icon != null && resourceType2.Icon != null)
            {
                if (resourceType1.Icon.height == resourceType2.Icon.height &&
                    resourceType1.Icon.width == resourceType2.Icon.width)
                {
                    for (int i=0;i< resourceType1.Icon.height;i++)
                    {
                        for (int j = 0; j < resourceType1.Icon.width; j++)
                        {
                            if (resourceType1.Icon.GetPixel(i, j) != resourceType2.Icon.GetPixel(i, j))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (resourceType1.Icon != null && resourceType2.Icon == null||
                     resourceType1.Icon == null && resourceType2.Icon != null)
            {
                return false;
            }

            return (resourceType1.Name == resourceType2.Name&&
                    resourceType1.Volatile == resourceType2.Volatile);
        }

        public static bool operator !=(ResourceType resourceType1, ResourceType resourceType2)
        {
            return !(resourceType1 == resourceType2);
        }

        public bool Equals(ResourceType other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ResourceType);
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
