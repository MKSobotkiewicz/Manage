using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceValue:IEquatable<ResourceValue>,IConvertible,ICloneable
    {
        public ResourceType ResourceType { get; private set; }
        public UInt64 Value { get; private set; }

        public ResourceValue(ResourceType resourceType, UInt64 value)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourceValue of ResourceType named " + resourceType.Name + " with value "+value.ToString() + " created.");
            ResourceType = resourceType;
            Value = value;
        }

        public void AddValue(UInt64 value)
        {
            Value += value;
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Added value " + value.ToString() + " to ResourceValue of ResourceType named " + ResourceType.Name + " for a total value of "+Value.ToString() + ".");
        }

        public void RemoveValue(UInt64 value)
        {
            Value -= value;
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Removed value " + value.ToString() + " from ResourceValue of ResourceType named " + ResourceType.Name + " for a total value of " + Value.ToString() + ".");
        }

        public void MultiplyValue(double value)
        {
            Value = (uint)((double)Value*value);
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "Multiplied value by " + value.ToString() + " from ResourceValue of ResourceType named " + ResourceType.Name + " for a total value of " + Value.ToString() + ".");
        }

        public static ResourceValue operator +( ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            CheckResourceTypes(resourceValue1, resourceValue2);
            ResourceValue output = resourceValue1.Clone() as ResourceValue;
            output.AddValue(resourceValue2.Value);
            return output;
        }

        public static ResourceValue operator +(ResourceValue resourceValue, UInt64 value)
        {
            ResourceValue output = resourceValue.Clone() as ResourceValue;
            output.AddValue(value);
            return output;
        }

        public static ResourceValue operator -(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            CheckResourceTypes(resourceValue1, resourceValue2);
            ResourceValue output = resourceValue1.Clone() as ResourceValue;
            output.RemoveValue(resourceValue2.Value);
            return output;
        }

        public static ResourceValue operator -(ResourceValue resourceValue, UInt64 value)
        {
            ResourceValue output = resourceValue.Clone() as ResourceValue;
            output.RemoveValue(value);
            return output;
        }

        public static ResourceValue operator *(ResourceValue resourceValue, double value)
        {
            ResourceValue output = resourceValue.Clone() as ResourceValue;
            output.MultiplyValue(value);
            return output;
        }

        public static bool operator >(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            CheckResourceTypes(resourceValue1, resourceValue2);
            return resourceValue1.Value > resourceValue2.Value;
        }

        public static bool operator <(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            CheckResourceTypes(resourceValue1, resourceValue2);
            return resourceValue1.Value < resourceValue2.Value;
        }

        public static bool operator ==(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            return (resourceValue1.Value == resourceValue2.Value && 
                    resourceValue1.ResourceType == resourceValue2.ResourceType);
        }

        public static bool operator !=(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            return (resourceValue1.Value != resourceValue2.Value ||
                    resourceValue1.ResourceType != resourceValue2.ResourceType);
        }

        public bool Equals(ResourceValue resourceValue)
        {
            return base.Equals(resourceValue);
        }

        public override bool Equals(Object obj)
        {
            return Equals(obj as ResourceValue);
        }

        public override int GetHashCode()
        {
            return (this as Object).GetHashCode();
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public Object Clone()
        {
            return new ResourceValue(ResourceType, Value)as Object;
        }

        private static void CheckResourceTypes(ResourceValue resourceValue1, ResourceValue resourceValue2)
        {
            if (resourceValue1.ResourceType != resourceValue2.ResourceType)
            {
                throw new InvalidOperationException("Different resource types.");
            }
        }

        public override string ToString()
        {
            return Value+" of "+ResourceType.Name;
        }
    }
}
