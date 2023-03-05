using UnityEngine;

namespace ResourcesMining
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] public Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int MiningHash = Animator.StringToHash("Mining");

        private void Update()
        {
            _animator.SetFloat(MoveHash, _characterMove.MovementMagnitude, 0.1f, Time.deltaTime);
        }

        public void PlayMining() => 
            _animator.SetTrigger(MiningHash);

        public void StopMining() => 
            _animator.ResetTrigger(MiningHash);
    }
}