using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Shot_Shift.Actors.Enemy.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private EnemyConfigSource _enemyConfig;
        private PauseService _pauseService;
        
        private Transform _target;
        private NavMeshAgent _agent;
        private bool _isAttacking;

        [Inject]
        private void Constructor(PauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void Setup(EnemyConfigSource enemyConfig, GameObject target)
        {
            _enemyConfig = enemyConfig;
            _target = target.transform;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _enemyConfig.Speed;
            _agent.stoppingDistance = _enemyConfig.AttackDistance - 1;
        }

        private void Update()
        {
            if(_pauseService.IsPaused)
            {
                _agent.ResetPath();
                return;
            }
            
            if(_target != null && gameObject.activeSelf)
            {
                _agent.SetDestination(_target.position);

                if (Vector3.Distance(_target.position, transform.position) <= _enemyConfig.AttackDistance)
                {
                    AttackTarget();
                }
            }
        }

        private void AttackTarget()
        {
            if (!_isAttacking && _target.GetComponent<IDamageable>() is {} damageable)
            {
                _isAttacking = true;
                damageable.TakeDamage(_enemyConfig.Damage);
                
                Invoke(nameof(CooldownAttackEnded), _enemyConfig.CooldownAttack);
            }
        }

        private void CooldownAttackEnded()
        {
            _isAttacking = false;
        }
    }
}
