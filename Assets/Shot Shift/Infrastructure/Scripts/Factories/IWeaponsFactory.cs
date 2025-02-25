using R3;
using Shot_Shift.Actors.Weapon.Scripts;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public interface IWeaponsFactory
    {
        Observable<Unit> InitializeFactory();
        void CreateWeapons(Transform weaponSpot);
        IWeaponController GetWeapon();
        GameObject GetBullet();
        void DisposeBullet(GameObject bullet);
    }
}