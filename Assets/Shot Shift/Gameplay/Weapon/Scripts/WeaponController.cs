using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Gameplay.Weapon.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController<TConfig> : MonoBehaviour, IWeaponController where TConfig : WeaponConfigSource
    {
        [SerializeField] protected TConfig _weaponConfig;
        [SerializeField] protected Transform _shootPoint;

        private AudioSource _audioSource;
        
        protected IWeaponsFactory _weaponsFactory;
        protected PlayerProgressService _playerProgressService;
        private SettingsService _settingsService;

        public WeaponConfigSource WeaponConfig => _weaponConfig;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory, PlayerProgressService playerProgressService, SettingsService settingsService)
        {
            _settingsService = settingsService;
            _playerProgressService = playerProgressService;
            _weaponsFactory = weaponsFactory;
            
            _audioSource = GetComponent<AudioSource>();
        }
        
        public virtual Vector3 FireWithRecoil()
        {
            SetupBullet();

            if(_settingsService.SoundStatus)
            {
                _audioSource.clip = _weaponConfig.ShotSound;
                _audioSource.Play();
            }
            
            Vector3 recoilDirection = -_shootPoint.right;
            return recoilDirection * (_weaponConfig.RecoilForce * _playerProgressService.RecoilUpgrade);
        }
        
        protected virtual void SetupBullet()
        {
            GameObject bulletPref = _weaponsFactory.GetBullet();
            bulletPref.transform.position = _shootPoint.position;
            bulletPref.transform.rotation = _shootPoint.rotation;
            bulletPref.SetActive(true);
            
            BulletController bullet = bulletPref.GetComponent<BulletController>();
            bullet.Setup(_weaponConfig.BulletConfig.BulletDamage * _playerProgressService.DamageUpgrade, _weaponConfig.BulletConfig.BulletSpeed, _weaponConfig.BulletConfig.BulletRange);
        }
    }

    public interface IWeaponController
    {
        Vector3 FireWithRecoil();
        WeaponConfigSource WeaponConfig { get; }
    }
}
