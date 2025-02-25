using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";
        protected const string INTERACT_BUTTON = "Fire1";
        protected const string SWITCH_WEAPON = "SwitchWeapon";
    
        public abstract Vector2 MoveAxis { get; }
        public abstract Vector2 RotateAxis { get; }
    
        public abstract bool Interact { get; }
        public abstract bool SwitchWeapon { get; }
    }

}