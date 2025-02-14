using System;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts
{
    public class LevelProgressWatcher : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void RunLevel()
        {
            Debug.Log("LevelProgressWatcher.RunLevel");
        }
    }
    
    public interface ILevelProgressService
    {
        LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        void InitForLevel(LevelProgressWatcher levelController);
    }
    
    public class LevelProgressService : ILevelProgressService
    {
        public LevelProgressWatcher LevelProgressWatcher { get; set; }
        
        public void InitForLevel(LevelProgressWatcher levelController) => 
            LevelProgressWatcher = levelController;
    }
    
    public class LevelProgressServiceResolver : IInitializable, IDisposable
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly LevelProgressWatcher _levelProgressWatcher;
        
        public LevelProgressServiceResolver(
            ILevelProgressService levelProgressService,
            [Inject(Source = InjectSources.Local, Optional = true)] LevelProgressWatcher levelProgressWatcher)
        {
            _levelProgressService = levelProgressService;
            _levelProgressWatcher = levelProgressWatcher;
        }
        
        public void Initialize() => 
            _levelProgressService.InitForLevel(_levelProgressWatcher);

        public void Dispose() => 
            _levelProgressService.InitForLevel(null);
    }

}
