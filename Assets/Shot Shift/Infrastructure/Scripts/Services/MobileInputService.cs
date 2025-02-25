using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class MobileInputService : InputService
    {
        private const string MOUSE_X = "X";
        private const string MOUSE_Y = "Y";
        
        public override Vector2 MoveAxis => SimpleMoveAxis();
        public override Vector2 RotateAxis => SimpleRotateAxis();
        public override bool Interact => SimpleInput.GetButton(INTERACT_BUTTON);
        public override bool SwitchWeapon => SimpleInput.GetButtonDown(SWITCH_WEAPON);

        private static Vector2 SimpleMoveAxis() => new(SimpleInput.GetAxis(HORIZONTAL), SimpleInput.GetAxis(VERTICAL));
        private static Vector2 SimpleRotateAxis() => new(SimpleInput.GetAxis(MOUSE_X), SimpleInput.GetAxis(MOUSE_Y));
    }
}