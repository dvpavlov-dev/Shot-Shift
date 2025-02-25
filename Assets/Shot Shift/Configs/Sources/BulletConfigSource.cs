using UnityEngine;
namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/Bullets/Bullet Config")]
    public class BulletConfigSource : ScriptableObject
    {
        [SerializeField] private float _bulletDamage;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletRange;
        
        public float BulletDamage => _bulletDamage;
        public float BulletSpeed => _bulletSpeed;
        public float BulletRange => _bulletRange;
    }
}
