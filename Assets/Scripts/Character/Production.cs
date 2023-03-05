using System.Collections.Generic;
using UnityEngine;

namespace ResourcesMining
{
    public class Production : MonoBehaviour
    {
        private GameFactory _factory;
        private ProgressService _progressService;
        private IInputService _inputService;
        private Transform _target;
        
        private List<ResourceCount> _resources;
        private float _spread;
        private float _delay;

        private bool _isProductionActive;
        private bool _hasResources = true;
        private float _currentCooldown = 0;

        public void Construct(GameFactory factory, ProgressService progressService, IInputService inputService)
        {
            _factory = factory;
            _progressService = progressService;
            _inputService = inputService;
        }

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;

            if (_isProductionActive == true && _hasResources == true && _currentCooldown <= 0)
            {
                if (_inputService.IsMovePressed() == true)
                    return;

                SendResourcesToSpot();
                _currentCooldown = _delay;
            }
        }

        private void SendResourcesToSpot()
        {
            bool hasResources = false;
            foreach (ResourceCount resource in _resources)
            {
                int count = _progressService.PlayerProgress.GetResource(resource.ResourceType).Count;

                if (count > 0)
                {
                    hasResources = true;
                    _progressService.ChangeResource(resource.ResourceType, -1);
                    GameResource newResource = _factory.CreateResourceToSpot(resource.ResourceType, transform.position);
                    newResource.StartImpulse(_spread);
                    newResource.MoveTo(_target);
                }
            }

            if (hasResources == false)
                _hasResources = hasResources;
        }

        public void SetProductionSettings(float spread, float delay, List<ResourceCount> resources)
        {
            _spread = spread;
            _delay = delay;
            _resources = resources;
        }

        public void SetTarget(Transform target) => 
            _target = target;

        public void EnableProduction() => 
            _isProductionActive = true;

        public void DisableProduction()
        {
            _isProductionActive = false;

            _hasResources = true;
            _currentCooldown = 0;
        }
    }
}