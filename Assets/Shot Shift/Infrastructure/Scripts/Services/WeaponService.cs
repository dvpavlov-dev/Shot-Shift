using System.Collections.Generic;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class WeaponService : IWeaponService
    {
        private Configs _configs;
        private PlayerProgressService _playerProgressService;
        
        private readonly List<IWeaponController> _activeWeapons = new();

        [Inject]
        private void Constructor(Configs configs, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _configs = configs;
        }

        public List<IWeaponController> GetActiveWeapons()
        {
            _activeWeapons.Clear();

            foreach (GameObject weapon in _configs.WeaponsConfig.Weapons)
            {
                IWeaponController weaponController = weapon.GetComponent<IWeaponController>();
                
                if (weaponController != null && weaponController.WeaponConfig.OpenAfterLevel <=_playerProgressService.LastCompletedLevel )
                {
                    _activeWeapons.Add(weaponController);
                }
            }

            return _activeWeapons;
        }
    }
    
    public interface IWeaponService
    {
        List<IWeaponController> GetActiveWeapons();
    }
}
