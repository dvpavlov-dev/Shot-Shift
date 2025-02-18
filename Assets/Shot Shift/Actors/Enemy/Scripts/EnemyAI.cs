using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using UnityEngine;
using UnityEngine.AI;

namespace Shot_Shift.Actors.Enemy.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private EnemyConfigSource _enemyConfig;
        private bool _isCanAttack;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _enemyConfig.Speed;
            _agent.stoppingDistance = _enemyConfig.AttackDistance - 1;
        }

        private void Update()
        {
            if(_target != null)
            {
                _agent.SetDestination(_target.position);

                if (_agent.remainingDistance <= _enemyConfig.AttackDistance)
                {
                    AttackTarget();
                }
            }
        }
        
        private void AttackTarget()
        {
            if (_isCanAttack && _target.GetComponent<IDamageable>() is {} damageable)
            {
                _isCanAttack = false;
                damageable.TakeDamage(_enemyConfig.Damage);
                
                Invoke(nameof(CooldownAttackEnded), _enemyConfig.CooldownAttack);
            }
        }

        private void CooldownAttackEnded()
        {
            _isCanAttack = true;
        }

        public void Initialize(EnemyConfigSource enemyConfig, GameObject target)
        {
            _enemyConfig = enemyConfig;
            _target = target.transform;
        }
    }
}
