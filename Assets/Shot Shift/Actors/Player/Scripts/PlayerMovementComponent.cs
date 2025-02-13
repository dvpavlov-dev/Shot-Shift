using Infrastructure;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Player.Scripts
{
    public class PlayerMovementComponent : MonoBehaviour
    {
        private IInputService _inputService;
        
        [Inject]
        private void Constructor(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 inputDirection = new Vector3(_inputService.RotateAxis.x, 0f, _inputService.RotateAxis.y).normalized;

            if (inputDirection.magnitude >= 0.1f)
            {
                Vector3 moveDirection = Quaternion.Euler(0, 90, 0) * inputDirection; 
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }
}
