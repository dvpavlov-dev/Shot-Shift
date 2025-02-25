using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using Shot_Shift.Actors.Enemy.Scripts;
using Shot_Shift.Actors.Player.Scripts;
using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class ActorsFactory : IActorsFactory
    {
        private const float DISTANCE_BEHIND_CAMERA = 30f;
        
        private readonly DiContainer _container;
        private readonly Configs _configs;
        private readonly PlayerProgressService _playerProgress;
        private readonly Queue<GameObject> _enemiesPool = new();
        
        private GameObject _player;
        private CompositeDisposable _disposable = new();
        private Transform _containerForEnemy;

        public ActorsFactory(DiContainer container, Configs configs, PlayerProgressService playerProgress)
        {
            _container = container;
            _configs = configs;
            _playerProgress = playerProgress;

            Application.quitting += OnGameQuit;
        }

        public Observable<Unit> InitializeFactory()
        {
            _disposable = new CompositeDisposable();
            
            return Observable.FromAsync(async cancellationToken =>
            {
                await CreateEnemyPool(cancellationToken);
            });
        }

        public GameObject CreatePlayer()
        {
            _player = _container.InstantiatePrefab(_configs.PlayerConfig.PlayerPrefab ,new Vector3(0, 0, 0), Quaternion.identity, _containerForEnemy);
            _player.GetComponent<Player>().Setup(_configs.PlayerConfig);
            
            return _player;
        }

        public GameObject GetEnemy()
        {
            GameObject enemy = _enemiesPool.Count == 0 ? CreateEnemy() : _enemiesPool.Dequeue();
            GameObject target = _player != null ? _player : CreatePlayer();
            
            enemy.transform.position = new Vector3(target.transform.position.x + DISTANCE_BEHIND_CAMERA, 0, 0);
            enemy.GetComponent<IEnemy>().Setup(_configs.EnemyConfig, target, this);
            enemy.SetActive(true);

            return enemy;
        }

        public void DisposeEnemy(GameObject enemy)
        {
            enemy.SetActive(false);
            _enemiesPool.Enqueue(enemy);
        }

        private async ValueTask CreateEnemyPool(CancellationToken cancellationToken)
        {
            _containerForEnemy = new GameObject("EnemiesPool").transform;
            _enemiesPool.Clear();
            
            for (int i = 0; i < _configs.LevelsConfig.Levels[_playerProgress.CurrentLevel].EnemyCount; i++)
            {
                GameObject enemy = CreateEnemy();
                enemy.SetActive(false);

                _enemiesPool.Enqueue(enemy);

                Debug.Log($"Create enemy: {i}");
                
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

        private GameObject CreateEnemy()
        {
            return _container.InstantiatePrefab(_configs.EnemyConfig.EnemyPrefab, _containerForEnemy);
        }
        
        private void OnGameQuit()
        {
            Application.quitting -= OnGameQuit;
            _disposable.Dispose();
        }
    }

}
