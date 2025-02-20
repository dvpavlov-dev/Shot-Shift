using System;
using UnityEngine;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class DamageController : MonoBehaviour, IDamageable
    {
        public Action<float> OnHealthChanged { get; set; }
        public Action OnDeath { get; set; }
        
        private float _health;

        public void Setup(float maxHealth)
        {
            _health = maxHealth;
        }
        
        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0f)
            {
                _health = 0f;
                OnDeath?.Invoke();
            }
            
            OnHealthChanged?.Invoke(_health);
        }
    }

    public interface IDamageable
    {
        Action<float> OnHealthChanged { get; set; }
        Action OnDeath { get; set; }
        void Setup(float maxHealth);
        void TakeDamage(float damage);
    }
}
