using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "ShotgunConfig", menuName = "Configs/Weapons/ShotgunConfig")]
    public class ShotgunConfigSource : WeaponConfigSource
    {
        [SerializeField] private float _spread;
        
        public float Spread => _spread;
    }
}
