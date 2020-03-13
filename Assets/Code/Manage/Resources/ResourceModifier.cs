using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceModifier
    {
        public ResourceType AffectedResource { get; private set; }
        public double Value { get; private set; }

        public ResourceModifier(ResourceType affectedResource, double value)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourceModifier of ResourceType "+affectedResource.Name+" and value "+value.ToString()+" created.");
            AffectedResource = affectedResource;
            Value = value;
        }
    }
}
