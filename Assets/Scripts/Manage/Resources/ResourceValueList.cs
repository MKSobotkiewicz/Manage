using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceValueList : List<ResourceValue>, IEquatable<ResourceValueList>, ICloneable
    {
        public ResourceValueList()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourceValueList created.");
        }

        public ResourceValue Find(ResourceType resourceType)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Looking for resource " + resourceType.Name + " in ResourceValueList.");
            for (int i = 0; i < Count; i++)
            {
                if (this[i].ResourceType == resourceType)
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Resource found.");
                    return this[i];
                }
            }
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Resource not found.");
            return null;
        }

        public Object Clone()
        {
            var output = new ResourceValueList { };
            foreach (var resourceValue in this)
            {
                output.Add(resourceValue.Clone() as ResourceValue);
            }
            return output as Object;
        }

        public static ResourceValueList operator *(ResourceValueList resourceValueList, double value)
        {
            var output = resourceValueList.Clone() as ResourceValueList;
            for (int i = 0; i < output.Count; i++)
            {
                output[i] *= value;
            }
            return output;
        }

        public static ResourceValueList operator *(double value, ResourceValueList resourceValueList)
        {
            return resourceValueList* value;
        }

        public static ResourceValueList operator +(ResourceValueList resourceValueList1, ResourceValueList resourceValueList2)
        {
            var output = resourceValueList1.Clone() as ResourceValueList;
            foreach (var ResourceValue2 in resourceValueList2)
            {
                bool wasPresent = false;
                foreach (var ResourceValue1 in output)
                {
                    if (ResourceValue1.ResourceType== ResourceValue2.ResourceType)
                    {
                        ResourceValue1.AddValue(ResourceValue2.Value);
                        wasPresent = true;
                        break;
                    }
                }
                if (!wasPresent)
                {
                    output.Add(ResourceValue2.Clone() as ResourceValue);
                }
            }
            return output;
        }

        public static ResourceValueList operator -(ResourceValueList resourceValueList1, ResourceValueList resourceValueList2)
        {
            var output = resourceValueList1.Clone() as ResourceValueList;
            foreach (var ResourceValue1 in output)
            {
                foreach (var ResourceValue2 in resourceValueList2)
                {
                    if (ResourceValue1.ResourceType == ResourceValue2.ResourceType)
                    {
                        ResourceValue1.RemoveValue(ResourceValue2.Value);
                        break;
                    }
                }
            }
            return output;
        }

        public static bool operator ==(ResourceValueList resourceValueList1, ResourceValueList resourceValueList2)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Comparing resource values.");
            if (resourceValueList1.Count != resourceValueList2.Count)
            {
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Lenght differs: Lenght1: "+resourceValueList1.Count+" Lenght2: "+ resourceValueList2.Count);
                return false;
            }
            for (int i = 0; i < resourceValueList1.Count; i++)
            {
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Value1: "+ resourceValueList1[i].Value+" "+
                                                                    resourceValueList1[i].ResourceType.Name + 
                                                                    " Value2: "+ resourceValueList2[i].Value+" " +
                                                                    resourceValueList2[i].ResourceType.Name);
                if (resourceValueList1[i] != resourceValueList2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(ResourceValueList resourceValueList1, ResourceValueList resourceValueList2)
        {
            return !(resourceValueList1== resourceValueList2);
        }

        public bool Equals(ResourceValueList resourceValueList)
        {
            return base.Equals(resourceValueList);
        }

        public override bool Equals(Object obj)
        {
            return Equals(obj as ResourceValueList);
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }

        public override string ToString()
        {
            string output="";
            foreach (var resourceValue in this)
            {
                output += resourceValue.ToString()+"\n";
            }
            return output;
        }
    }
}
