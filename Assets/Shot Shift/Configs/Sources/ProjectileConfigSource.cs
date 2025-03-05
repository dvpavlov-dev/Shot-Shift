using UnityEngine;
using UnityEngine.Serialization;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/Bullets/Bullet Config")]
    public class ProjectileConfigSource : ScriptableObject
    {
        [SerializeField] private GameObject _projectilePrefab;
        [FormerlySerializedAs("_bulletDamage")]
        [SerializeField] private float _damage;
        [FormerlySerializedAs("_bulletSpeed")]
        [SerializeField] private float _speed;
        [FormerlySerializedAs("_bulletRange")]
        [SerializeField] private float _range;
        
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float Damage => _damage;
        public float Speed => _speed;
        public float Range => _range;
    }
}
