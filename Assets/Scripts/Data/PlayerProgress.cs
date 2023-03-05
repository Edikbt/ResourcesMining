using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourcesMining
{
    [Serializable]
    public class PlayerProgress
    {
        public List<ResourceCount> Resources;

        public PlayerProgress()
        {
            Resources = new List<ResourceCount>
            {
                new ResourceCount(ResourceTypeId.Crystal, 0),
                new ResourceCount(ResourceTypeId.Metall, 0),
                new ResourceCount(ResourceTypeId.Wood, 0),
            };
        }

        public ResourceCount GetResource(ResourceTypeId typeId) =>
            Resources.First(x => x.ResourceType == typeId);
    }
}