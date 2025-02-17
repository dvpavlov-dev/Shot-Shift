using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.UI.Scripts;
namespace Shot_Shift.Infrastructure.Scripts
{
    public class StartSceneState : IState
    {
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;

        public StartSceneState(ISceneLoaderService sceneLoaderService, ILoadingCurtains loadingCurtains)
        {
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading game...");
            _sceneLoaderService.LoadScene("Start", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }
        
        private void OnLoadedScene()
        {
            _loadingCurtains.HideLoadingCurtains();
        }
    }
}