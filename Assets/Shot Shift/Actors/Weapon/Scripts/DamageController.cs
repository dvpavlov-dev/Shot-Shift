using System;
using UnityEngine;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class DamageController : MonoBehaviour, IDamageable
    {
        public Action<float> OnHealthChanged { get; set; }
        public Action OnDeath { get; set; }
        
        [SerializeField] private float _health = 100f;

        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0f)
            {
                _health = 0f;
                OnDeath?.Invoke();
                
                Destroy(gameObject);
            }
            
            OnHealthChanged?.Invoke(_health);
        }
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}
