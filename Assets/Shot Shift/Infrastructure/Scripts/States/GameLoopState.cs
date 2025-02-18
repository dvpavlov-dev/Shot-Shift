using R3;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.UI.Scripts;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.States
{
    public class GameLoopState : IState
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IActorsFactory _actorsFactory;
        private readonly IBulletsFactory _bulletsFactory;

        public GameLoopState(ILevelProgressService levelProgressService, 
            ILoadingCurtains loadingCurtains, 
            ISceneLoaderService sceneLoaderService, 
            IActorsFactory actorsFactory,
            IBulletsFactory bulletsFactory)
        {
            _levelProgressService = levelProgressService;
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
            _actorsFactory = actorsFactory;
            _bulletsFactory = bulletsFactory;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading level...");
            _sceneLoaderService.LoadScene("GameLoop", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }

        private void OnInitializedEnded()
        {
            _loadingCurtains.HideLoadingCurtains();
            _levelProgressService.LevelProgressWatcher.RunLevel();
        }

        private void OnLoadedScene()
        {
            _loadingCurtains.UpdateDescriptionText("Loading enemies...");
            _actorsFactory.InitializeFactory().Subscribe(_ =>
            {
                _loadingCurtains.UpdateDescriptionText("Loading bullets...");
                _bulletsFactory.InitializeFactory().Subscribe(_ =>
                {
                    Debug.Log("Метод 2 завершен!");
                    OnInitializedEnded();
                });
            });
        }
    }
}