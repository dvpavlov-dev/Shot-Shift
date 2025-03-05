using System;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Gameplay.Weapon.Scripts.Projectiles
{
    public class BulletController<TProjectileConfig> : MonoBehaviour where TProjectileConfig : ProjectileConfigSource
    {
        [SerializeField] private TProjectileConfig _bulletConfig;
        
        private IWeaponsFactory _weaponsFactory;
        private AbilitiesService _abilitiesService;
        
        private float _damage;
        private float _speed;
        private float _range;

        private Vector3 _endPoint;
        private PlayerProgressService _playerProgressService;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory, AbilitiesService abilitiesService, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _abilitiesService = abilitiesService;
            _weaponsFactory = weaponsFactory;
        }

        private void OnEnable()
        {
            Setup(_bulletConfig.Damage * _playerProgressService.DamageUpgrade, _bulletConfig.Speed, _bulletConfig.Range);
        }

        private void Setup(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;

            _endPoint = transform.right * _range;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint, Time.deltaTime * _speed * _abilitiesService.SpeedCoefficient);
            
            if (Vector3.Distance(transform.position, _endPoint) < 1f)
            {
                Dispose();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IDamageable>() is {} damageable && !other.CompareTag("Player"))
            {
                damageable.TakeDamage(_damage);
                Dispose();
            }
        }

        private void Dispose()
        {
            _weaponsFactory.DisposeProjectile(_bulletConfig, gameObject);
        }
    }
}
