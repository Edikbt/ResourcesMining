using System.Collections;
using UnityEngine;

namespace ResourcesMining
{
    public class GameResource : MonoBehaviour
    {
        private const float UpForce = 7f;
        private const float MoveDelay = 1f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _pickupCollider;
        [SerializeField] private SphereCollider _absorbeCollider;
        [SerializeField] private MoveTo _moveTo;

        public ResourceTypeId ResourceType;

        private float _pickUpRadius;
        private float _pickUpDelay;

        public void Init(ResourceData resourceData)
        {
            ResourceType = resourceData.ResourceType;
            _pickUpRadius = resourceData.PickUpRadius;
            _pickUpDelay = resourceData.PickUpDelay;
            _pickupCollider.radius = _pickUpRadius;
        }

        public void StartImpulse(float impulse)
        {
            Vector3 force = new Vector3(Random.Range(-impulse, impulse), UpForce, Random.Range(-impulse, impulse));
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public void MoveTo(Transform target) =>
            StartCoroutine(MoveToDelay(target));

        public void StartPickUpDelay() =>
            StartCoroutine(PickUpDelay());

        private IEnumerator PickUpDelay()
        {
            yield return new WaitForSeconds(_pickUpDelay);
            _pickupCollider.enabled = true;
        }

        private IEnumerator MoveToDelay(Transform target)
        {
            _moveTo.SetTarget(target);
            _rigidbody.velocity = Vector3.zero;

            yield return new WaitForSeconds(MoveDelay);

            _absorbeCollider.enabled = true;
            _rigidbody.useGravity = false;
            _moveTo.enabled = true;
        }
    }
}