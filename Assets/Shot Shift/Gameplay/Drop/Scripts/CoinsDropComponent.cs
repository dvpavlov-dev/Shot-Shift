using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Gameplay.Drop.Scripts
{
    public class CoinsDropComponent : DropComponent
    {
        private PlayerProgressService _playerProgressService;
        
        [Inject]
        private void Constructor(PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }
        
        public override void TakeDrop(GameObject actor)
        {
            if (actor.CompareTag("Player"))
            {
                _playerProgressService.ChangeCoinsData(_playerProgressService.CoinsCount + 10);
                Destroy(gameObject);
            }
        }
    }
}
