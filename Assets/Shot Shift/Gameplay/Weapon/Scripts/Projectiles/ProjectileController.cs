using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Shot_Shift.Gameplay.Weapon.Scripts.Projectiles
{
    public class ProjectileController<TProjectileConfig> : MonoBehaviour where TProjectileConfig : ProjectileConfigSource
    {
        [FormerlySerializedAs("_bulletConfig")]
        [SerializeField] protected TProjectileConfig _projectileConfig;
        
        private IWeaponsFactory _weaponsFactory;
        private AbilitiesService _abilitiesService;
        
        protected float _damage;
        protected float _speed;
        protected float _range;

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
            Setup(_projectileConfig.Damage * _playerProgressService.DamageUpgrade, _projectileConfig.Speed, _projectileConfig.Range);
        }

        protected virtual void CollideWithObject(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Projectile"))
            {
                if (other.gameObject.GetComponent<IDamageable>() is {} damageable)
                {
                    damageable.TakeDamage(_damage);
                }
                
                Dispose();
            }
        }
        
        protected void Dispose()
        {
            _weaponsFactory.DisposeProjectile(_projectileConfig, gameObject);
        }

        private void Setup(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;

            _endPoint = transform.position + transform.right * _range;
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
            CollideWithObject(other);
        }
    }
}
