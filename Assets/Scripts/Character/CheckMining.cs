using UnityEngine;

namespace ResourcesMining
{
    public class CheckMining : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Mining _mining;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _mining.DisableMining();
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (obj.TryGetComponent(out Source source) && source.IsAvailable == true)
            {
                _mining.SetMiningSpeed(source.MiningSpeed);
                _mining.SetTarget(obj.transform);
                _mining.EnableMining();
            }
        }

        private void TriggerExit(Collider obj) => 
            _mining.DisableMining();
    }
}