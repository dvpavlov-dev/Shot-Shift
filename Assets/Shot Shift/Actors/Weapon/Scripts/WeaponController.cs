using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class WeaponController : MonoBehaviour, IWeaponController
    {
        [SerializeField] private WeaponConfigSource _weaponConfig;
        [SerializeField] private Transform _shootPoint;
        
        private IWeaponsFactory _weaponsFactory;

        public WeaponConfigSource WeaponConfig => _weaponConfig;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory)
        {
            _weaponsFactory = weaponsFactory;
        }
        
        public Vector3 FireWithRecoil()
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
