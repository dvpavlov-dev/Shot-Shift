using Shot_Shift.UI.Scripts.StartScene;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Installers
{
    public class StartSceneInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_startMenuController")]
        [SerializeField] private StartMenuUIController _startMenuUIController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_startMenuUIController).AsSingle().NonLazy();
        }
    }
}
