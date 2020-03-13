using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceModifierList:List<ResourceModifier>
    {
        public ResourceValue Modify(ResourceValue resourceValue)
        {
            double _modifiersSum = 1;
            foreach (var modifier in this)
            {
                if (modifier.AffectedResource== resourceValue.ResourceType)
                {
                    _modifiersSum += modifier.Value;
                }
            }
            return new ResourceValue(resourceValue.ResourceType, Convert.ToUInt64(resourceValue* _modifiersSum));
        }
    }
}
