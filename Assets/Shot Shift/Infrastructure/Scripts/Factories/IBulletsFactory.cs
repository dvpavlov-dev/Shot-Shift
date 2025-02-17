using R3;
using UnityEngine;
namespace Shot_Shift.Infrastructure.Scripts.Factories
{
    public interface IBulletsFactory
    {
        Observable<Unit> InitializeFactory();
        GameObject GetBullet();
        void DisposeBullet(GameObject bullet);
    }
}