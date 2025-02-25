using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class WeaponsFactory : IWeaponsFactory
    {
        private readonly DiContainer _container;
        private readonly Configs _configs;
        private readonly IWeaponService _weaponService;

        private readonly Queue<GameObject> _bulletsPool = new();
        private readonly List<GameObject> _weapons = new();

        private CompositeDisposable _disposable;
        private Transform _containerForBullets;
        private int _currentWeaponId;

        public WeaponsFactory(DiContainer container, Configs configs, IWeaponService weaponService)
        {
            _container = container;
            _weaponService = weaponService;
            _configs = configs;
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
            
            foreach (IWeaponController activeWeapon in _weaponService.GetActiveWeapons())
            {
                GameObject weapon = _container.InstantiatePrefab(activeWeapon.WeaponConfig.WeaponPref, weaponSpot);
                _weapons.Add(weapon);
            }
        }

        public IWeaponController GetWeapon()
        {
            if(_weapons.Count == 0) 
                return null;

            foreach (GameObject weapon in _weapons)
            {
                weapon.SetActive(false);
            }
            
            GameObject currentWeapon = _weapons[_currentWeaponId];
            currentWeapon.SetActive(true);

            _currentWeaponId = _currentWeaponId + 1 < _weapons.Count ? _currentWeaponId + 1 : 0;

            return currentWeapon.GetComponent<IWeaponController>();
        }

        public GameObject GetBullet()
        {
            GameObject bullet = _bulletsPool.Count == 0 ? CreateBullet(_containerForBullets) : _bulletsPool.Dequeue();
            
            return bullet;
        }

        public void DisposeBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            _bulletsPool.Enqueue(bullet);
        }

        private async ValueTask CreateBulletsPool(CancellationToken cancellationToken)
        {
            _containerForBullets = new GameObject("BulletsPool").transform;
            _bulletsPool.Clear();
            
            for (int i = 0; i < 10; i++)
            {
                GameObject bullet = CreateBullet(_containerForBullets);
                bullet.SetActive(false);

                _bulletsPool.Enqueue(bullet);

                Debug.Log($"Create bullet: {i}");
                
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

        private GameObject CreateBullet(Transform containerForBullets)
        {
            return _container.InstantiatePrefab(_configs.WeaponsConfig.BulletPref, containerForBullets);
        }
    }
}
