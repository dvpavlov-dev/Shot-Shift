using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using UnityEngine;

namespace Shot_Shift.Actors.Enemy.Scripts
{
    [RequireComponent(typeof(DamageController), typeof(EnemyAI))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        private IDamageable _damageController;
        private EnemyAI _enemyAI;
        private IActorsFactory _actorsFactory;
        
        public void Initialize(EnemyConfigSource enemyConfig, GameObject target, IActorsFactory actorsFactory)
        {
            _actorsFactory = actorsFactory;
            _damageController = GetComponent<DamageController>();
            _enemyAI = GetComponent<EnemyAI>();

            _damageController.OnDeath += Dispose;
            
            _enemyAI.Initialize(enemyConfig, target);
        }
        
        private void Dispose()
        {
            _damageController.OnDeath -= Dispose;
            _actorsFactory.DisposeEnemy(gameObject);
        }
    }
    
    public interface IEnemy
    {
        void Initialize(EnemyConfigSource enemyConfig, GameObject target, IActorsFactory actorsFactory);
    }
}
