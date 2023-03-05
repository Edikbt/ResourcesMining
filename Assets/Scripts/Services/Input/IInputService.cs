using UnityEngine;

namespace ResourcesMining
{
    public interface IInputService
    {
        Vector2 GetAxis();
        bool IsMovePressed();
    }
}