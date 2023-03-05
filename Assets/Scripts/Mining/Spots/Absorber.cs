using System;
using UnityEngine;

namespace ResourcesMining
{
    public class Absorber : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        public Action<ResourceTypeId> Absorbed;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
        }

        private void TriggerEnter(Collider obj)
        {
            GameResource resource = obj.GetComponentInParent<GameResource>();

            if (resource != null)
            {
                Absorbed?.Invoke(resource.ResourceType);
                Destroy(resource.gameObject);
            }
        }
    }
}