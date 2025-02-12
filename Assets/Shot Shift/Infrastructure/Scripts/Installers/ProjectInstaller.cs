using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
    #if UNITY_STANDALONE
            Container.Bind<IInputService>().FromInstance(new StandaloneInputService());
    #elif UNITY_ANDROID
            Container.Bind<IInputService>().FromInstance(new MobileInputService());
    #endif
        }
    }
}