using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class ActorsFactory : IActorsFactory
    {
        private readonly Configs _configs;
        private readonly Queue<GameObject> _enemiesPool = new();
        
        private GameObject _player;
        private CompositeDisposable _disposable = new();
        private Transform _containerForEnemy;

        public ActorsFactory(Configs configs)
        {
            _configs = configs;
            
            Application.quitting += OnGameQuit;
        }
        private void OnGameQuit()
        {
            Application.quitting -= OnGameQuit;
            _disposable.Dispose();
        }

        public void InitializeFactory(Action onInitializedEnded)
        {
            _disposable = new CompositeDisposable();
            
            if (_enemiesPool.Count == 0)
            {
                Observable.FromAsync(CreateEnemyPool)
                    .Subscribe(
                        onNext: _ =>
                        {
                            onInitializedEnded?.Invoke();
                        }
                    ).AddTo(_disposable);
            }
            else
            {
                onInitializedEnded?.Invoke();
            }
        }

        public GameObject CreatePlayer()
        {
            _player = Object.Instantiate(_configs.PlayerConfig.PlayerPrefab);
            return _player;
        }

        public GameObject GetEnemy()
        {
            GameObject enemy = _enemiesPool.Count == 0 ? CreateEnemy() : _enemiesPool.Dequeue();
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
            return Object.Instantiate(_configs.EnemyConfig.EnemyPrefab, _containerForEnemy);
        }
    }
    
    public interface IActorsFactory
    {
        void InitializeFactory(Action onInitializedEnded);
        GameObject CreatePlayer();
        GameObject GetEnemy();
        void DisposeEnemy(GameObject enemy);
    }
}
