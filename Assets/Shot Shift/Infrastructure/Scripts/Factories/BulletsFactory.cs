using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using R3;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public class BulletsFactory : IBulletsFactory
    {
        private readonly Configs _configs;
        private readonly Queue<GameObject> _bulletsPool = new Queue<GameObject>();
        private CompositeDisposable _disposable;

        public BulletsFactory(Configs configs)
        {
            _configs = configs;
        }

        public void InitializeFactory(Action onInitializedEnded)
        {
            _disposable = new CompositeDisposable();
            
            if (_bulletsPool.Count == 0)
            {
                Observable.FromAsync(CreateBulletsPool)
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

        public GameObject GetBullet()
        {
            GameObject bullet = _bulletsPool.Count == 0 ? CreateBullet() : _bulletsPool.Dequeue();
            
            return bullet;
        }

        public void DisposeBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            _bulletsPool.Enqueue(bullet);
        }

        private async ValueTask CreateBulletsPool(CancellationToken arg)
        {
            await Task.Yield();
        }
        
        private GameObject CreateBullet()
        {
            return null;
        }
    }
    
    public interface IBulletsFactory
    {
        void InitializeFactory(Action onInitializedEnded);
        GameObject GetBullet();
        void DisposeBullet(GameObject bullet);
    }
}
