using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using UnityEngine;

namespace Shot_Shift.Actors.Player.Scripts
{
    [RequireComponent(typeof(PlayerMovementComponent), typeof(PlayerAttackComponent), typeof(DamageController))]
    public class Player : MonoBehaviour
    {
        private PlayerAttackComponent _playerAttackComponent;
        private DamageController _damageController;
        
        public void Setup(PlayerConfigSource playerConfig)
        {
            _playerAttackComponent = GetComponent<PlayerAttackComponent>();
            _damageController = GetComponent<DamageController>();
            
            _playerAttackComponent.Setup();
            _damageController.Setup(playerConfig.Health);
        }
    }
}
