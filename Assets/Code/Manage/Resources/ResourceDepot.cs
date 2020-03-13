using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourceDepot: ResourceValueList
    {
        public ResourceDepot()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ResourceDepot created.");
        }

        public ResourceDepot(ResourceValueList resourceValueList)
        {
            foreach (var resourceValue in resourceValueList)
            {
                this.Add(resourceValue);
            }
        }

        public UInt64 CheckResourceRemovalCapacityPercentage(ResourceValueList resourcesToRemove, ResourceModifierList modifiers)
        {
            var capacities = new List<UInt64>();
            foreach (var resourceToRemove in resourcesToRemove)
            {
                var resource = Find(resourceToRemove.ResourceType);
                if (Equals(resource, null))
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.CheckResourceRemoveCapacity return 0.");
                    return 0;
                }
                if (modifiers.Modify(resourceToRemove) > resource)
                {
                    capacities.Add((resource.Value*100)/ modifiers.Modify(resourceToRemove).Value);
                }
            }
            UInt64 minCapacity = 100;
            foreach (var capacity in capacities)
            {
                if (capacity < minCapacity)
                {
                    minCapacity = capacity;
                }
            }
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.CheckResourceRemoveCapacity return "+ minCapacity.ToString() + ".");
            return minCapacity;
        }

        public UInt64 CheckResourceRemovalCapacityPercentage(ResourceValueList resourcesToRemove)
        {
            return CheckResourceRemovalCapacityPercentage(resourcesToRemove,new ResourceModifierList { });
        }

        public UInt64 CheckResourceRemovalCapacityPercentage(ResourceValue resourcesToAdd, ResourceModifierList modifiers)
        {
            return CheckResourceRemovalCapacityPercentage(new ResourceValueList { resourcesToAdd }, modifiers);
        }

        public UInt64 CheckResourceRemovalCapacityPercentage(ResourceValue resourcesToAdd)
        {
            return CheckResourceRemovalCapacityPercentage(resourcesToAdd, new ResourceModifierList());
        }

        public ResourceDepot Add(ResourceValueList resourcesToAdd, ResourceModifierList modifiers)
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot adds resource.");
            foreach (var resourceToAdd in resourcesToAdd)
            {
                var resource = Find(resourceToAdd.ResourceType);
                if (!ReferenceEquals(resource,null))
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.AddResources adds resource " +
                                                                        resourceToAdd.ResourceType.Name +
                                                                        " with quantity " +
                                                                        modifiers.Modify(resourceToAdd).Value.ToString() +
                                                                        " for a total value of "+
                                                                        (resource + modifiers.Modify(resourceToAdd)).Value.ToString() +
                                                                        ".");
                    resource.AddValue(modifiers.Modify(resourceToAdd).Value);
                }
                else
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.AddResources adds new resource " +
                                                                           resourceToAdd.ResourceType.Name +
                                                                           " with quantity " +
                                                                           modifiers.Modify(resourceToAdd).Value.ToString() +
                                                                           ".");
                    ((List<ResourceValue>)this).Add(modifiers.Modify(resourceToAdd));
                }
            }
            return this;
        }

        public ResourceDepot Add(ResourceValueList resourcesToAdd) 
        {
            return Add(resourcesToAdd,new ResourceModifierList());
        }

        public ResourceDepot Add(ResourceValue resourcesToAdd, ResourceModifierList modifiers)
        {
            return Add(new ResourceValueList { resourcesToAdd }, modifiers);
        }

        public new ResourceDepot Add(ResourceValue resourcesToAdd)
        {
            return Add(new ResourceValueList { resourcesToAdd }, new ResourceModifierList());
        }

        public ResourceDepot Remove(ResourceValueList resourcesToRemove, ResourceModifierList modifiers)
        {
            foreach (var resourceToRemove in resourcesToRemove)
            {
                var resource = Find(resourceToRemove.ResourceType);
                if (ReferenceEquals(resource, null))
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.RemoveResources - no resource " +
                                                                        resourceToRemove.ResourceType.Name.ToString() +
                                                                        " in ResourceDepot.");
                    return null;
                }
            }
            foreach (var resourceToRemove in resourcesToRemove)
            {
                var resource = Find(resourceToRemove.ResourceType);
                if (modifiers.Modify(resourceToRemove).Value > resource.Value)
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.RemoveResources - not enough resource "+ 
                                                                        resourceToRemove .ResourceType.Name.ToString()+ 
                                                                        " in ResourceDepot.");
                    return null;
                }
            }
            foreach (var resourceToRemove in resourcesToRemove)
            {
                var resource = Find(resourceToRemove.ResourceType);
                Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.RemoveResources - removed resource from ResourceDepot.");
                resource.RemoveValue( modifiers.Modify(resourceToRemove).Value);
                if (resource.Value == 0)
                {
                    Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel3, "ResourceDepot.RemoveResources return true - removed all of resource from ResourceDepot.");
                    ((List<ResourceValue>)this).Remove(resource);
                }
            }
            return this;
        }

        public ResourceDepot Remove(ResourceValueList resourcesToRemove)
        {
            return Remove(resourcesToRemove, new ResourceModifierList());
        }

        public ResourceDepot Remove(ResourceValue resourcesToRemove, ResourceModifierList modifiers)
        {
            return Remove(new ResourceValueList { resourcesToRemove }, modifiers);
        }

        public ResourceDepot RemoveResources(ResourceValue resourcesToRemove)
        {
            return Remove(new ResourceValueList { resourcesToRemove }, new ResourceModifierList());
        }
    }
}
