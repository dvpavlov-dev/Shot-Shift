using UnityEngine;
using UnityEngine.AI;

namespace Shot_Shift.Actors.Enemy.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        private NavMeshAgent _agent;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            _agent.SetDestination(target.position);
        }
    }
}
