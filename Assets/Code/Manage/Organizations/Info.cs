using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Organizations
{
    public class Info : IEquatable<Info>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Texture2D Flag { get; private set; }
        public Color Color { get; private set; }

        public Info(string name, string description, Texture2D flag,Color color)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type Info with name: >>" + name + "<< and description: >>" + description + "<< created.");
            Name = name;
            Description = description;
            Flag = flag;
            Color = color;
        }

        public static bool operator ==(Info info1, Info info2)
        {
            if (Equals(info1, null)&& Equals(info2, null))
            {
                return true;
            }
            if (Equals(info1, null) || Equals(info2, null))
            {
                return false;
            }
            if (!Equals( info1.Flag , null) && !Equals(info2.Flag, null))
            {
                if (info1.Flag.width == info2.Flag.width &&
                   info1.Flag.height == info2.Flag.height)
                {
                    for (int i=0;i< info1.Flag.width;i++)
                    {
                        for (int j = 0; j < info1.Flag.width; j++)
                        {
                            if (info1.Flag.GetPixel(i, j) != info2.Flag.GetPixel(i, j))
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
            else if (info1.Flag != null && info2.Flag == null ||
                     info1.Flag == null && info2.Flag != null)
            {
                return false;
            }
            return (info1.Name == info2.Name &&
                    info1.Description == info2.Description&&
                    info1.Color == info2.Color);
        }

        public static bool operator !=(Info info1, Info info2)
        {
            return !(info1 == info2);
        }

        public bool Equals(Info info)
        {
            return base.Equals(info);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Info);
        }
    }
}
