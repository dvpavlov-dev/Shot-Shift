using UnityEngine;

namespace Infrastructure
{
    public class StandaloneInputService : InputService
    {
        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        
        public override Vector2 MoveAxis => UnityMoveAxis();
        public override Vector2 RotateAxis => UnityRotateAxis();
        public override bool Interact => Input.GetButtonDown(INTERACT_BUTTON);

        private static Vector2 UnityMoveAxis() => new(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
        private static Vector2 UnityRotateAxis() => new Vector2(Input.GetAxis(MOUSE_X), Input.GetAxis(MOUSE_Y));

    }
}