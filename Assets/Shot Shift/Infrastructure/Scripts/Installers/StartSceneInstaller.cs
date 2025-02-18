using Shot_Shift.UI.Scripts.StartScene;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Installers
{
    public class StartSceneInstaller : MonoInstaller
    {
        [SerializeField] private StartMenuController _startMenuController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_startMenuController).AsSingle().NonLazy();
        }
    }
}
