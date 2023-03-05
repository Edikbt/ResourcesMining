using UnityEngine;

namespace ResourcesMining
{
    public class Mining : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;

        private float _miningSpeed = 1;
        private float _currentCooldown = 0;

        private bool _isMiningActive = false;
        private Transform _target;

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;

            if (_isMiningActive == true && _currentCooldown <= 0)
            {
                StartMining();
                _currentCooldown = _miningSpeed;
            }
        }

        // Animation callback
        private void OnMiningHit()
        {
            if (_target != null)
            {
                bool isExhausted = _target.GetComponent<Source>().Hit();

                if (isExhausted == true)
                    DisableMining();
            }
        }

        public void SetMiningSpeed(float miningSpeed)
        {
            if (miningSpeed > 0)
                _miningSpeed = miningSpeed;
        }

        public void SetTarget(Transform target) => 
            _target = target;

        public void EnableMining() => 
            _isMiningActive = true;

        public void DisableMining()
        {
            _isMiningActive = false;
            _currentCooldown = 0;
            _animator.StopMining();
        }

        private void StartMining() => 
            _animator.PlayMining();
    }
}