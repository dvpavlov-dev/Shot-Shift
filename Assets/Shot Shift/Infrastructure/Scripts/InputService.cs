using UnityEngine;

namespace Infrastructure
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";
        protected const string INTERACT_BUTTON = "Fire1";
    
        public abstract Vector2 MoveAxis { get; }
        public abstract Vector2 RotateAxis { get; }
    
        public abstract bool Interact { get; }
    }
    
    public interface IInputService
    {
        public Vector2 MoveAxis { get; }
        public Vector2 RotateAxis { get; }
        public bool Interact { get; }
    }
}