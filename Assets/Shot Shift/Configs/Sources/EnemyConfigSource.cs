using UnityEngine;
namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfigSource : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float Health;
        public float Damage;
        public float CooldownAttack;
        public float AttackDistance;
        public float Speed;
    }
}
