using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private LevelProgressWatcher levelProgressWatcher;
        
        public override void InstallBindings()
        {
            Container.BindInstance(levelProgressWatcher);
        }
    }
}
