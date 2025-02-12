using Infrastructure;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Player.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAttackComponent : MonoBehaviour
    {
        public float recoilForce = 2f;
        public Transform shootingPoint;
        public GameObject bulletPrefab;

        private Vector3 movement;
        private Vector3 recoilVelocity;
    
        private Rigidbody _rb;
        private IInputService _inputService;

        [Inject]
        private void Constructor(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_inputService.Interact)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

            Vector3 recoilDirection = -shootingPoint.forward; // Направление отдачи
            _rb.AddForce(recoilDirection * recoilForce, ForceMode.Impulse);
        }
    }

}
