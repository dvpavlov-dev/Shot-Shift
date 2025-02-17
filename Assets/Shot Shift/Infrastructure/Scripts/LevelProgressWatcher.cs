using System;
using R3;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts
{
    public class LevelProgressWatcher : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private EnemySpawnerService _enemySpawnerService = new();
        private IActorsFactory _actorsFactory;
        private Configs _configs;
        private PlayerProgressService _playerProgressService;
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IActorsFactory actorsFactory, Configs configs, PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
            _configs = configs;
            _actorsFactory = actorsFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void RunLevel()
        {
            Debug.Log("LevelProgressWatcher.RunLevel");
            _actorsFactory.CreatePlayer();
            _enemySpawnerService.SpawnEnemy(_actorsFactory, _configs.LevelsConfig.levels[_playerProgressService.CurrentLevel], _disposable);
            _enemySpawnerService.LevelFinished = OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            Debug.Log("LevelProgressWatcher.LevelFinished");
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}
