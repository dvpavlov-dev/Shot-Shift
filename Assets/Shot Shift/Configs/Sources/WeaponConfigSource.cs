using UnityEngine;
using UnityEngine.Serialization;

namespace Shot_Shift.Configs.Sources
{
    public abstract class WeaponConfigSource : ScriptableObject
    {
        [Header("Weapon parameters")]
        [SerializeField] private GameObject _weaponPref;
        [SerializeField] private float _recoilForce;
        [SerializeField] private float _shotsPerSeconds;
        [SerializeField] private int _openAfterLevel;
        [SerializeField] private AudioClip _shotSound;

        [FormerlySerializedAs("_projectilePrefab")]
        [FormerlySerializedAs("_bulletPrefab")]
        [FormerlySerializedAs("_bulletConfig")]
        [Header("Use bullets")]
        [SerializeField] private ProjectileConfigSource _projectileConfig;

        public GameObject WeaponPref => _weaponPref;
        public float RecoilForce => _recoilForce;
        public float ShotsPerSecond => _shotsPerSeconds;
        public int OpenAfterLevel => _openAfterLevel;
        public ProjectileConfigSource ProjectileConfig => _projectileConfig;
        public AudioClip ShotSound => _shotSound;
    }
}
