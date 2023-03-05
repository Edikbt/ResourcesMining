using UnityEngine;

namespace ResourcesMining
{
    public class CheckProduction : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Production _production;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (obj.TryGetComponent(out Spot spot))
            {
                _production.SetTarget(obj.transform);
                _production.SetProductionSettings(spot.Spread, spot.Delay, spot.RequiredResources);
                _production.EnableProduction();
            }
        }

        private void TriggerExit(Collider obj) => 
            _production.DisableProduction();
    }
}