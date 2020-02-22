using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceModifierSpeedList : List<double>
    {
        public ulong Modify(UInt64 value)
        {
            double modifiersSum = 1;
            foreach (var modifier in this)
            {
                modifiersSum += modifier;
            }
            return Convert.ToUInt64(value * modifiersSum);
        }
    }
}
