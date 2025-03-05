using R3;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Gameplay.Weapon.Scripts;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public interface IWeaponsFactory
    {
        Observable<Unit> InitializeFactory();
        void CreateWeapons(Transform weaponSpot);
        IWeaponController GetFirstWeapon();
        IWeaponController GetNextWeapon();
        GameObject GetProjectile(ProjectileConfigSource projectilePrefab);
        void DisposeProjectile(ProjectileConfigSource projectileConfig, GameObject projectile);
    }
}