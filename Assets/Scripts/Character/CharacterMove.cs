using UnityEngine;
using UnityEngine.AI;

namespace ResourcesMining
{
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _movementSpeed;
        
        private const float JoystickSensitivityThreshold = 0.001f;

        public float MovementMagnitude { get; private set; }

        private IInputService _inputService;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            MovementMagnitude = 0;

            if (_inputService.GetAxis().sqrMagnitude > JoystickSensitivityThreshold)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.GetAxis());
                movementVector.y = 0;
                MovementMagnitude = movementVector.magnitude;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _navMeshAgent.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}