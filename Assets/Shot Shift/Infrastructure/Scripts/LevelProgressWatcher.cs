using System;
using R3;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.UI.Scripts.GameLoopScene;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts
{
    public class LevelProgressWatcher : MonoBehaviour
    {
        [SerializeField] private GameLoopUIController _gameLoopUIController;
        
        private EnemySpawnerService _enemySpawnerService = new();
        private IActorsFactory _actorsFactory;
        private Configs _configs;
        private PlayerProgressService _playerProgressService;
        private PauseService _pauseService;

        private CompositeDisposable _disposable = new();
        private IDamageable _playerDamageable;
        private IDisposable _timerObserver;

        [Inject]
        private void Construct(IActorsFactory actorsFactory, Configs configs, PlayerProgressService playerProgressService, PauseService pauseService)
        {
            _pauseService = pauseService;
            _playerProgressService = playerProgressService;
            _configs = configs;
            _actorsFactory = actorsFactory;
        }

        public void RunLevel()
        {
            Debug.Log("LevelProgressWatcher.RunLevel");

            LevelsConfigSource.Level currentLevelConfig = _configs.LevelsConfig.levels[_playerProgressService.CurrentLevel];
            _pauseService.IsPaused = false;
            
            HudSetup(currentLevelConfig);
            PlayerSetup();
            EnemySpawnerSetup();
        }
        
        private void HudSetup(LevelsConfigSource.Level currentLevelConfig)
        {
            _gameLoopUIController.HudView.SetupHud(_configs.PlayerConfig.Health, currentLevelConfig.IsTimerNeeded ? currentLevelConfig.TimerIntervalInSeconds : 0);

            if (currentLevelConfig.IsTimerNeeded)
            {
                StartTimer(currentLevelConfig.TimerIntervalInSeconds);
            }
        }
        
        private void EnemySpawnerSetup()
        {
            _enemySpawnerService.SpawnEnemy(_actorsFactory, _configs.LevelsConfig.levels[_playerProgressService.CurrentLevel], _disposable);
            _enemySpawnerService.LevelFinished += OnLevelFinished;
        }
        
        private void PlayerSetup()
        {
            GameObject player = _actorsFactory.CreatePlayer();
            _playerDamageable = player.GetComponent<IDamageable>();
            _playerDamageable.OnDeath += OnLevelLost;
            _playerDamageable.OnHealthChanged = OnPlayerHealthChanged;
        }

        private void StartTimer(int startSeconds)
        {
            _timerObserver = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    if(--startSeconds > 0)
                    {
                        UpdateTimer(startSeconds);
                    }
                    else
                    {
                        UpdateTimer(0);
                        OnLevelLost();
                        _timerObserver.Dispose();
                    }
                })
                .AddTo(_disposable);
        }

        private void OnPlayerHealthChanged(float health)
        {
            _gameLoopUIController.HudView.UpdateHealth(health);
        }

        private void UpdateTimer(int seconds)
        {
            _gameLoopUIController.HudView.UpdateTimer(seconds);
        }
        
        private void OnLevelLost()
        {
            Debug.Log("LevelProgressWatcher.LevelLost");
            
            _playerDamageable.OnDeath -= OnLevelLost;
            _gameLoopUIController.ShowLoseWindow();
            _pauseService.IsPaused = true;
        }

        private void OnLevelFinished()
        {
            Debug.Log("LevelProgressWatcher.LevelFinished");
            
            _enemySpawnerService.LevelFinished -= OnLevelFinished;
            _playerProgressService.ChangeLevelData(_playerProgressService.CurrentLevel + 1);
            
            _gameLoopUIController.ShowWinningWindow();
            _pauseService.IsPaused = true;
        }

        private void OnDestroy()
        {
            _timerObserver?.Dispose();
            _disposable.Dispose();
        }
    }
}
