using UnityEngine;

namespace ResourcesMining
{
    public class MoveTo: MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Transform _target;

        private void Update()
        {
            if (_target != null)
            {
                Vector3 movementVector = _target.position - transform.position;
                transform.Translate(_speed * movementVector.normalized * Time.deltaTime);
            }
        }

        public void SetTarget(Transform target) =>
            _target = target;
    }
}