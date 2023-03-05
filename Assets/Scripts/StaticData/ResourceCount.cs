using System;

namespace ResourcesMining
{
    [Serializable]
    public class ResourceCount
    {
        public ResourceTypeId ResourceType;
        public int Count;

        public ResourceCount(ResourceTypeId resourceType, int count)
        {
            ResourceType = resourceType;
            Count = count;
        }
    }
}