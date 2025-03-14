using System.Collections;
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
        private Coroutine _fireIsOverCoroutine;

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
                _fireIsOverCoroutine = StartCoroutine(FireIsOver());
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
            StopCoroutine(_fireIsOverCoroutine);
            _isFire = false;
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

        private IEnumerator FireIsOver()
        {
            yield return new WaitForSeconds(1 / (_currentWeapon.WeaponConfig.ShotsPerSecond * _abilitiesService.SpeedCoefficient));
            _isFire = false;
        }

        private void Shoot()
        {
            Vector3 recoilDirection = _currentWeapon.FireWithRecoil();
            
            _rb.AddForce(recoilDirection, ForceMode.Impulse);
        }
    }
}
