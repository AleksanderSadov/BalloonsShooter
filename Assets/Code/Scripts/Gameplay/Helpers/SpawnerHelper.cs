using BalloonsShooter.Core;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class SpawnerHelper<T> : ISpawner<T> where T : MonoBehaviour
    {
        private readonly ObjectPoolHelper<T> objectPool;

        public SpawnerHelper(
            T objectPrefab,
            int defaultCapacity,
            int maxCapacity
        )
        {
            objectPool = new(
                objectPrefab: objectPrefab,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxCapacity: maxCapacity
            );
        }

        ~SpawnerHelper()
        {
            objectPool.pool.Dispose();
        }

        public T Spawn()
        {
            T entity = objectPool.pool.Get();
            return entity;
        }

        public void Kill(T entity)
        {
            objectPool.pool.Release(entity);
        }

        public void FreeSpawner()
        {
            objectPool.pool.Dispose();
        }
    }
}

