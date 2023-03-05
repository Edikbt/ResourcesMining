using UnityEngine;

namespace ResourcesMining
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 GetAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));

        public bool IsMovePressed() => 
            Input.GetMouseButton(0);
    }
}