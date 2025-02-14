using Shot_Shift.UI.Scripts;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtains _loadingCurtains;
        
        public override void InstallBindings()
        {
            Container.Bind<ILoadingCurtains>().FromInstance(_loadingCurtains).AsSingle().NonLazy();
        }
    }
}