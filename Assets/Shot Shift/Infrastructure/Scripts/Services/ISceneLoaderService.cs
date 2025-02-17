using System;
namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public interface ISceneLoaderService
    {
        void LoadScene(string sceneName, Action onSceneLoaded = null);
    }
}