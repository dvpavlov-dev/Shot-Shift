using Shot_Shift.Infrastructure.Scripts.States;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<StartSceneState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle(); //GameStateMachine entry point is Initialize()
        }
    }
}
