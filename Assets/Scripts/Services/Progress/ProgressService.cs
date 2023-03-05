using System;

namespace ResourcesMining
{
    public class ProgressService
    {
        public PlayerProgress PlayerProgress;
        public Action<ResourceTypeId, int> ResourceCountChanged;

        public void ChangeResource(ResourceTypeId resourceType, int value)
        {
            PlayerProgress.GetResource(resourceType).Count += value;
            ResourceCountChanged?.Invoke(resourceType, value);
        }
    }
}