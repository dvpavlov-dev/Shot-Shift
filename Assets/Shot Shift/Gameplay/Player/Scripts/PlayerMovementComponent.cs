using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Gameplay.Player.Scripts
{
    public class PlayerMovementComponent : MonoBehaviour
    {
        private IInputService _inputService;
        private PauseService _pauseService;

        [Inject]
        private void Constructor(IInputService inputService, PauseService pauseService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }

        private void Update()
        {
            if(_pauseService.IsPaused)
                return;
            
            Vector3 inputDirection = new Vector3(_inputService.RotateAxis.x, 0f, _inputService.RotateAxis.y).normalized;
            
            if (inputDirection.magnitude >= 0.1f)
            {
                Vector3 moveDirection = Quaternion.Euler(0, 90, 0) * inputDirection; 
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }
}
