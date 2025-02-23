using Infrastructure;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Actors.Player.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAttackComponent : MonoBehaviour
    {
        public float recoilForce = 2f;
        public Transform shootingPoint;
        public GameObject bulletPrefab;

        private IInputService _inputService;
        private PauseService _pauseService;
        
        private Vector3 movement;
        private Vector3 recoilVelocity;
        private Rigidbody _rb;
        private bool _isFire;

        [Inject]
        private void Constructor(IInputService inputService, PauseService pauseService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_pauseService.IsPaused && _inputService.Interact && !_isFire)
            {
                Shoot();
                
                _isFire = true;
                Invoke(nameof(FireIsOver), 0.2f);
            }
        }
        
        private void FireIsOver()
        {
            _isFire = false;
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

            Vector3 recoilDirection = -shootingPoint.right;
            _rb.AddForce(recoilDirection * recoilForce, ForceMode.Impulse);
        }
    }

}
