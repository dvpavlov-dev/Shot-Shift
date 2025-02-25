using System.Collections.Generic;
using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfigSource : ScriptableObject
    {
        [SerializeField] private List<GameObject> _weapons;
        [SerializeField] private GameObject _bulletPref;
        
        public List<GameObject> Weapons => _weapons;
        public GameObject BulletPref => _bulletPref;
    }
}
