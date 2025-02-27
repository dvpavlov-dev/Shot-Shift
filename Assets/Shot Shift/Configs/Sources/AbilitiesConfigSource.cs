using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "Configs/AbilitiesConfig")]
    public class AbilitiesConfigSource : ScriptableObject
    {
        [SerializeField] private BulletTimeConfigSource _bulletTimeConfig;
        
        public BulletTimeConfigSource BulletTimeConfig => _bulletTimeConfig;
    }
}
