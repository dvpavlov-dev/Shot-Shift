using Shot_Shift.Configs.Sources;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts
{
    public class Configs : MonoBehaviour
    {
        [SerializeField] private PlayerConfigSource _playerConfig;
        [SerializeField] private EnemyConfigSource _enemyConfig;
        [SerializeField] private LevelsConfigSource _levelsConfig;
        [SerializeField] private WeaponsConfigSource _weaponsConfig;
        
        public PlayerConfigSource PlayerConfig => _playerConfig;
        public EnemyConfigSource EnemyConfig => _enemyConfig;
        public LevelsConfigSource LevelsConfig => _levelsConfig;
        public WeaponsConfigSource WeaponsConfig => _weaponsConfig;
    }
}
