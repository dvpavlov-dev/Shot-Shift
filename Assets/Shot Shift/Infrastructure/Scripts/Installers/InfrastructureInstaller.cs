using Infrastructure;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private Configs _configs;
    
        public override void InstallBindings()
        {
            Container.Bind<Configs>().FromInstance(_configs);
        
            BindServices();
            BindFactories();
        }
    
        private void BindServices()
        {
            Container.Bind<PauseService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelProgressServiceResolver>()
                .AsSingle()
                .CopyIntoDirectSubContainers();
            Container.BindInterfacesAndSelfTo<LevelProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle().NonLazy();
            BindInputService();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.BindInterfacesTo<ActorsFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponsFactory>().AsSingle().NonLazy();
        }
    
        private void BindInputService()
        {
        #if UNITY_STANDALONE
            Container.Bind<IInputService>().FromInstance(new StandaloneInputService()).AsSingle().NonLazy();
        #elif UNITY_ANDROID
            Container.Bind<IInputService>().FromInstance(new MobileInputService()).AsSingle().NonLazy();
        #endif
        }
    }
}
