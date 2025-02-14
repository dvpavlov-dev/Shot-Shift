using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private CompositeDisposable _disposable;
    
        public SceneLoaderService()
        {
            Application.quitting += OnGameQuit;
        }
    
        public void LoadScene(string sceneName, Action onSceneLoaded = null)
        {
            _disposable = new CompositeDisposable();
            
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => 
                {
                    var waitNextScene = SceneManager.LoadSceneAsync(sceneName);
                    
                    Observable
                        .EveryUpdate()
                        .Subscribe(_ =>
                        {
                            if (waitNextScene.isDone)
                            {
                                onSceneLoaded?.Invoke();
                                _disposable.Dispose();
                            }
                        })
                        .AddTo(_disposable);
                })
                .AddTo(_disposable); 
        }

        private void OnGameQuit()
        {
            Application.quitting -= OnGameQuit;
            _disposable.Dispose();
        }
    }
    
    public interface ISceneLoaderService
    {
        void LoadScene(string sceneName, Action onSceneLoaded = null);
    }
}
