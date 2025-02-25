using Shot_Shift.Infrastructure.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class BulletController : MonoBehaviour
    {
        private float _damage;
        private float _speed;
        private float _range;
        private IWeaponsFactory _weaponsFactory;

        [Inject]
        private void Constructor(IWeaponsFactory weaponsFactory)
        {
            _weaponsFactory = weaponsFactory;
        }
        
        public void Setup(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;

            Invoke(nameof(Dispose), _range / _speed);
        }

        private void Update()
        {
            transform.Translate(transform.right * Time.deltaTime * _speed, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IDamageable>() is {} damageable && !other.CompareTag("Player"))
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        private void Dispose()
        {
            _weaponsFactory.DisposeBullet(gameObject);
        }
    }
}
