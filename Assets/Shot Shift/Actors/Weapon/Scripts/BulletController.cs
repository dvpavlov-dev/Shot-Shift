using UnityEngine;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class BulletController : MonoBehaviour
    {
        private float _damage;
        private float _speed;
        private float _range;
        
        public void Setup(float damage, float speed, float range)
        {
            _damage = damage;
            _speed = speed;
            _range = range;

            Destroy(gameObject, _range / _speed);
        }

        void Update()
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
    }
}
