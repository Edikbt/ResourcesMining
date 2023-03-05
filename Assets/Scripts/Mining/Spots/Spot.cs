using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    public class Spot : MonoBehaviour
    {
        [SerializeField] private Emitter _emitter;
        [SerializeField] private Absorber _absorbe;
        
        public float Spread { get; private set; }
        public float Delay { get; private set; }
        public List<ResourceCount> RequiredResources { get; private set; }

        private List<ResourceCount> _createdResources;
        private Dictionary<ResourceTypeId, int> _absorbedResources = new Dictionary<ResourceTypeId, int>();

        private void Start()
        {
            _absorbe.Absorbed += AbsorbeResource;
        }

        private void OnDestroy()
        {
            _absorbe.Absorbed -= AbsorbeResource;
        }

        public void Init(SpotData spotData)
        {
            Delay = spotData.Delay;
            Spread = spotData.Spread;

            RequiredResources = spotData.RequiredResources;
            _createdResources = spotData.CreatedResources;
        }

        private void AbsorbeResource(ResourceTypeId resourceId)
        {
            if (_absorbedResources.ContainsKey(resourceId))
                _absorbedResources[resourceId]++;
            else
                _absorbedResources.Add(resourceId, 1);

            CheckEmitting();
        }

        private void CheckEmitting()
        {
            if (IsEnoughResources() == true)
            {
                _emitter.Emitt(_createdResources);
                DecreaseAbsorbedResources();
            }
        }

        private void DecreaseAbsorbedResources()
        {
            foreach (ResourceCount resource in RequiredResources)
                _absorbedResources[resource.ResourceType] -= resource.Count;
        }

        private bool IsEnoughResources()
        {
            foreach (ResourceCount resource in RequiredResources)
                if (_absorbedResources[resource.ResourceType] < resource.Count)
                    return false;

            return true;
        }
    }
}