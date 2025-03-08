using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Gameplay.Weapon.Scripts;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class WeaponsFactory : IWeaponsFactory
    {
        private readonly DiContainer _container;
        private readonly IWeaponService _weaponService;

        private readonly Dictionary<ProjectileConfigSource, Queue<GameObject>> _projectilesPools = new();
        private readonly List<IWeaponController> _activeWeaponsControllers = new();
        private readonly List<GameObject> _weapons = new();

        private CompositeDisposable _disposable;
        private Transform _containerForProjectiles;
        private int _currentWeaponId;

        public WeaponsFactory(DiContainer container, IWeaponService weaponService)
        {
            _container = container;
            _weaponService = weaponService;
        }

        public Observable<Unit> InitializeFactory()
        {
            return Observable.FromAsync(async cancellationToken =>
            {
                await CreateBulletsPool(cancellationToken);
            });
        }
        
        public void CreateWeapons(Transform weaponSpot)
        {
            _weapons.Clear();
            
            foreach (IWeaponController activeWeapon in _activeWeaponsControllers)
            {
                GameObject weapon = _container.InstantiatePrefab(activeWeapon.WeaponConfig.WeaponPref, weaponSpot);
                _weapons.Add(weapon);
            }
        }

        public IWeaponController GetFirstWeapon()
        {
            _currentWeaponId = 0;
            return GetWeapon(_currentWeaponId);
        }

        public IWeaponController GetNextWeapon()
        {
            _currentWeaponId = _currentWeaponId + 1 < _weapons.Count ? _currentWeaponId + 1 : 0;
            
            return GetWeapon(_currentWeaponId);
        }
        
        public GameObject GetProjectile(ProjectileConfigSource projectilePrefab)
        {
            GameObject projectile = _projectilesPools[projectilePrefab].Count == 0 ? CreateProjectile(projectilePrefab.ProjectilePrefab, _containerForProjectiles) : _projectilesPools[projectilePrefab].Dequeue();
            projectile.SetActive(false);
            
            return projectile;
        }

        public void DisposeProjectile(ProjectileConfigSource projectileConfig, GameObject projectile)
        {
            projectile.SetActive(false);
            _projectilesPools[projectileConfig].Enqueue(projectile);
        }
        
        private IWeaponController GetWeapon(int weaponId)
        {
            if(_weapons.Count == 0) 
                return null;

            foreach (GameObject weapon in _weapons)
            {
                weapon.SetActive(false);
            }
            
            var currentWeapon = _weapons[weaponId];
            currentWeapon.SetActive(true);
            
            return currentWeapon.GetComponent<IWeaponController>();
        }

        private async ValueTask CreateBulletsPool(CancellationToken cancellationToken)
        {
            _containerForProjectiles = new GameObject("ProjectilesPool").transform;
            _activeWeaponsControllers.Clear();
            _projectilesPools.Clear();
            
            foreach (IWeaponController weapon in _weaponService.GetActiveWeapons())
            {
                _activeWeaponsControllers.Add(weapon);
                ProjectileConfigSource projectilePref = weapon.WeaponConfig.ProjectileConfig;
                
                if (!_projectilesPools.ContainsKey(projectilePref))
                {
                    _projectilesPools.Add(projectilePref, new Queue<GameObject>());
                }
                
                for (int i = 0; i < 10; i++)
                {
                    GameObject projectile = CreateProjectile(projectilePref.ProjectilePrefab, _containerForProjectiles);
                    projectile.SetActive(false);

                    _projectilesPools[projectilePref].Enqueue(projectile);

                    Debug.Log($"Create projectile: {i}");
                
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Yield();
                }
            }
        }

        private GameObject CreateProjectile(GameObject projectilePref, Transform containerForBullets)
        {
            return _container.InstantiatePrefab(projectilePref, containerForBullets);
        }
    }
}
