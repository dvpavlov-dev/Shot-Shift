using Shot_Shift.Configs.Sources;
using Shot_Shift.Gameplay.Weapon.Scripts;
using Shot_Shift.Infrastructure.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class WeaponController<TConfig> : MonoBehaviour, IWeaponController where TConfig : WeaponConfigSource
    {
        [SerializeField] protected TConfig _weaponConfig;
        [SerializeField] protected Transform _shootPoint;
        
        protected IWeaponsFactory _weaponsFactory;

        public WeaponConfigSource WeaponConfig => _weaponConfig;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory)
        {
            _weaponsFactory = weaponsFactory;
        }
        
        public virtual Vector3 FireWithRecoil()
        {
            GameObject bulletPref = _weaponsFactory.GetBullet();
            bulletPref.transform.position = _shootPoint.position;
            bulletPref.transform.rotation = _shootPoint.rotation;
            bulletPref.SetActive(true);
            
            BulletController bullet = bulletPref.GetComponent<BulletController>();
            bullet.Setup(_weaponConfig.BulletConfig.BulletDamage, _weaponConfig.BulletConfig.BulletSpeed, _weaponConfig.BulletConfig.BulletRange);

            Vector3 recoilDirection = -_shootPoint.right;
            return recoilDirection * _weaponConfig.RecoilForce;
        }
    }

    public interface IWeaponController
    {
        Vector3 FireWithRecoil();
        WeaponConfigSource WeaponConfig { get; }
    }
}
