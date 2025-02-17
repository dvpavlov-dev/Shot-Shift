using UnityEngine;
using UnityEngine.AI;

namespace Shot_Shift.Actors.Enemy.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private Transform _target;
        private NavMeshAgent _agent;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(_target != null)
            {
                _agent.SetDestination(_target.position);
            }
        }

        public void Initialize(GameObject target)
        {
            _target = target.transform;
        }
    }
}
