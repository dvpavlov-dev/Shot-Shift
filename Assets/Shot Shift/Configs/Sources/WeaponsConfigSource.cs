using System.Collections.Generic;
using Shot_Shift.Actors.Weapon.Scripts;
using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfigSource : ScriptableObject
    {
        [SerializeField] private List<WeaponController> _weapons;
        [SerializeField] private GameObject _bulletPref;
        
        public List<WeaponController> Weapons => _weapons;
        public GameObject BulletPref => _bulletPref;
    }
}
