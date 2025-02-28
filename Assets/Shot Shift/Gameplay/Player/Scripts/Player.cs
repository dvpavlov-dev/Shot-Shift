using Shot_Shift.Actors.Player.Scripts;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Gameplay.Player.Scripts
{
    [RequireComponent(typeof(PlayerMovementComponent), typeof(PlayerAttackComponent), typeof(DamageController))]
    public class Player : MonoBehaviour
    {
        private PlayerAttackComponent _playerAttackComponent;
        private DamageController _damageController;
        private PlayerProgressService _playerProgressService;

        [Inject]
        private void Constructor(PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }
        
        public void Setup(PlayerConfigSource playerConfig)
        {
            _playerAttackComponent = GetComponent<PlayerAttackComponent>();
            _damageController = GetComponent<DamageController>();
            
            _playerAttackComponent.Setup();
            _damageController.Setup(playerConfig.Health * _playerProgressService.HealthUpgrade);
        }
    }
}
