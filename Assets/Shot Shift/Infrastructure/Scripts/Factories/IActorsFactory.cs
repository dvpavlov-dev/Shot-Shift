using R3;
using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public interface IActorsFactory
    {
        Observable<Unit> InitializeFactory();
        GameObject CreatePlayer();
        GameObject GetEnemy();
        void DisposeEnemy(GameObject enemy);
    }
}