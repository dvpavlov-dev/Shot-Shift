using System.Collections.Generic;
using Shot_Shift.Actors.Weapon.Scripts;
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

            foreach (IWeaponController weapon in _configs.WeaponsConfig.Weapons)
            {
                if (weapon.WeaponConfig.OpenAfterLevel <=_playerProgressService.LastCompletedLevel )
                {
                    _activeWeapons.Add(weapon);
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
