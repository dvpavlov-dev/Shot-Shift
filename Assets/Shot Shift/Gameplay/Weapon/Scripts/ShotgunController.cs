using Shot_Shift.Configs.Sources;
using UnityEngine;

namespace Shot_Shift.Gameplay.Weapon.Scripts
{
    public class ShotgunController : WeaponController<ShotgunConfigSource>
    {
        protected override void SetupBullet()
        {
            for (int i = 0; i < _weaponConfig.NumberOfBilletsFired; i++)
            {
                GameObject bulletPref = _weaponsFactory.GetBullet();
                bulletPref.transform.position = _shootPoint.position;
                bulletPref.transform.rotation = _shootPoint.rotation;
                bulletPref.transform.Rotate(new Vector3(360f / _weaponConfig.NumberOfBilletsFired * i, 0, 15));
                bulletPref.SetActive(true);

                BulletController bullet = bulletPref.GetComponent<BulletController>();
                bullet.Setup(_weaponConfig.BulletConfig.BulletDamage * _playerProgressService.DamageUpgrade, _weaponConfig.BulletConfig.BulletSpeed, _weaponConfig.BulletConfig.BulletRange);
            }
        }
    }
}
