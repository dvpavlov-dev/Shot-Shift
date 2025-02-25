using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    public abstract class WeaponConfigSource : ScriptableObject
    {
        [Header("Weapon parameters")]
        [SerializeField] private GameObject _weaponPref;
        [SerializeField] private float _recoilForce;
        [SerializeField] private float _shotsPerSeconds;
        [SerializeField] private int _openAfterLevel;

        [Header("Use bullets")]
        [SerializeField] private BulletConfigSource _bulletConfig;

        public GameObject WeaponPref => _weaponPref;
        public float RecoilForce => _recoilForce;
        public float ShotsPerSecond => _shotsPerSeconds;
        public int OpenAfterLevel => _openAfterLevel;
        public BulletConfigSource BulletConfig => _bulletConfig;
    }

}
