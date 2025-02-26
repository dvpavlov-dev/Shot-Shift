using Shot_Shift.Gameplay.Drop.Scripts;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class DropFactory : IDropFactory
    {
        private Configs _configs;
        private DiContainer _container;

        [Inject]
        private void Constructor(DiContainer container, Configs configs)
        {
            _container = container;
            _configs = configs;
        }
        
        public GameObject CreateDrop(DropType dropType)
        {
            switch (dropType)
            {
                case DropType.MEDKIT:
                    return CreateMedkitDrop();
                
                case DropType.COINS:
                    return CreateCoinsDrop();
                
                default:
                    return null;
            }
        }

        private GameObject CreateMedkitDrop()
        {
            GameObject drop = Object.Instantiate(_configs.DropsConfig.MedkitDropPrefab);
            return drop;
        }

        private GameObject CreateCoinsDrop()
        {
            GameObject drop = Object.Instantiate(_configs.DropsConfig.CoinsDropPrefab);
            return drop;
        }
    }
    
    public interface IDropFactory
    {
        GameObject CreateDrop(DropType dropType);
    }

    public enum DropType
    {
        MEDKIT,
        COINS,
    }
}
