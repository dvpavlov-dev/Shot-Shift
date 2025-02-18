using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using Shot_Shift.Actors.Enemy.Scripts;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class ActorsFactory : IActorsFactory
    {
        private readonly DiContainer _container;
        private readonly Configs _configs;
        private readonly Queue<GameObject> _enemiesPool = new();
        
        private GameObject _player;
        private CompositeDisposable _disposable = new();
        private Transform _containerForEnemy;

        public ActorsFactory(DiContainer container, Configs configs)
        {
            _container = container;
            _configs = configs;
            
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
            _player ??= _container.InstantiatePrefab(_configs.PlayerConfig.PlayerPrefab ,new Vector3(0, 0, 0), Quaternion.identity, null);
            return _player;
        }

        public GameObject GetEnemy()
        {
            GameObject enemy = _enemiesPool.Count == 0 ? CreateEnemy() : _enemiesPool.Dequeue();
            enemy.GetComponent<IEnemy>().Initialize(_configs.EnemyConfig, _player, this);
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
            
            for (int i = 0; i < _configs.LevelsConfig.levels[0].EnemyCount; i++)
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
