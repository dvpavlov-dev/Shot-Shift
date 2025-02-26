using System;
using UnityEngine;

namespace Shot_Shift.Actors.Weapon.Scripts
{
    public class DamageController : MonoBehaviour, IDamageable
    {
        public Action<float> OnHealthChanged { get; set; }
        public Action<IDamageable> OnDeath { get; set; }
        
        private float _maxHealth;
        private float _health;

        public void Setup(float maxHealth)
        {
            _maxHealth = _health = maxHealth;
        }
        
        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0f)
            {
                _health = 0f;
                OnDeath?.Invoke(this);
            }
            
            OnHealthChanged?.Invoke(_health);
        }

        public void TakeHealing(float healing)
        {
            _health += healing;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            
            OnHealthChanged?.Invoke(_health);
        }
    }

    public interface IDamageable
    {
        Action<float> OnHealthChanged { get; set; }
        Action<IDamageable> OnDeath { get; set; }
        void Setup(float maxHealth);
        void TakeDamage(float damage);
        void TakeHealing(float healing);
    }
}
