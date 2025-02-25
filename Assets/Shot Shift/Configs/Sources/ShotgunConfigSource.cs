using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "ShotgunConfig", menuName = "Configs/Weapons/ShotgunConfig")]
    public class ShotgunConfigSource : WeaponConfigSource
    {
        [Header("Shotgun parameters")]
        [SerializeField] private float _spread;
        [SerializeField] private int _numberOfBilletsFired;
        
        public float Spread => _spread;
        public int NumberOfBilletsFired => _numberOfBilletsFired;
    }
}
