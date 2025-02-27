using Shot_Shift.Configs.Sources;
using Shot_Shift.Gameplay.Weapon.Scripts;
using UnityEngine;
namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class ShotgunController : WeaponController<ShotgunConfigSource>
    {
        public override Vector3 FireWithRecoil()
        {
            for (int i = 0; i < _weaponConfig.NumberOfBilletsFired; i++)
            {
                GameObject bulletPref = _weaponsFactory.GetBullet();
                bulletPref.transform.position = _shootPoint.position;
                bulletPref.transform.rotation = _shootPoint.rotation;
                bulletPref.transform.Rotate(new Vector3(360f/_weaponConfig.NumberOfBilletsFired * i, 0, 15));
                bulletPref.SetActive(true);
            
                BulletController bullet = bulletPref.GetComponent<BulletController>();
                bullet.Setup(_weaponConfig.BulletConfig.BulletDamage, _weaponConfig.BulletConfig.BulletSpeed, _weaponConfig.BulletConfig.BulletRange);
            }
            
            Vector3 recoilDirection = -_shootPoint.right;
            return recoilDirection * _weaponConfig.RecoilForce;
        }
    }
}
