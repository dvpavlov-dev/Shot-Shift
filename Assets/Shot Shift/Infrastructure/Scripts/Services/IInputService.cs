using UnityEngine;
namespace Infrastructure
{
    public interface IInputService
    {
        public Vector2 MoveAxis { get; }
        public Vector2 RotateAxis { get; }
        public bool Interact { get; }
    }
}