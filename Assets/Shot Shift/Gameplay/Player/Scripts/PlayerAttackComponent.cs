using Shot_Shift.Gameplay.Weapon.Scripts;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Actors.Player.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAttackComponent : MonoBehaviour
    {
        [SerializeField] private Transform _weaponSpot;
        
        private IInputService _inputService;
        private PauseService _pauseService;
        private IWeaponsFactory _weaponsFactory;
        private AbilitiesService _abilitiesService;

        private IWeaponController _currentWeapon;
        private int _currentWeaponId;
        private Rigidbody _rb;
        private bool _isFire;
        private bool _canWeaponChange = true;

        [Inject]
        private void Constructor(
            IInputService inputService, 
            PauseService pauseService, 
            IWeaponsFactory weaponsFactory,
            AbilitiesService abilitiesService)
        {
            _abilitiesService = abilitiesService;
            _weaponsFactory = weaponsFactory;
            _pauseService = pauseService;
            _inputService = inputService;
        }

        public void Setup()
        {
            _rb = GetComponent<Rigidbody>();
            _weaponsFactory.CreateWeapons(_weaponSpot);

            _currentWeapon = _weaponsFactory.GetFirstWeapon();
        }

        private void Update()
        {
            if(_pauseService.IsPaused)
                return;
            
            if (_inputService.Interact && !_isFire)
            {
                Shoot();
                
                _isFire = true;
                Invoke(nameof(FireIsOver), 1 / _currentWeapon.WeaponConfig.ShotsPerSecond);
            }

            if (_inputService.SwitchWeapon)
            {
                SwitchWeapon();
            }

            if (_inputService.UseAbility)
            {
                UseBulletTime();
            }
        }
        
        private void SwitchWeapon()
        {
            if (!_canWeaponChange) 
                return;

            _canWeaponChange = false;
            _currentWeapon = _weaponsFactory.GetNextWeapon();
            FireIsOver();
            Invoke(nameof(ChangeWeaponActivated), 0.5f);
        }

        private void ChangeWeaponActivated()
        {
            _canWeaponChange = true;
        }
        
        private void UseBulletTime()
        {
            _abilitiesService.ActivateBulletTime();
        }

        private void FireIsOver()
        {
            _isFire = false;
        }

        private void Shoot()
        {
            Vector3 recoilDirection = _currentWeapon.FireWithRecoil();
            
            _rb.AddForce(recoilDirection, ForceMode.Impulse);
        }
    }
}
