using UnityEngine;
using UnityEngine.Serialization;
namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfigSource : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float Health;
    }
}
