using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class PlaneSpawnerHelper<T> : ISpawner<T> where T : MonoBehaviour
    {
        private readonly SpawnerHelper<T> spawnerHelper;
        private readonly IRandomSpawnPosition spawnerPositionHelper;

        public PlaneSpawnerHelper(
            T objectPrefab,
            Transform planeSpawner,
            int defaultCapacity,
            int maxCapacity
        )
        {
            spawnerHelper = new(
                objectPrefab: objectPrefab,
                defaultCapacity: defaultCapacity,
                maxCapacity: maxCapacity
            );

            spawnerPositionHelper = new PlaneSpawnerPositionVector(planeSpawner);
        }

        public T Spawn()
        {
            T entity = spawnerHelper.Spawn();
            entity.transform.position = spawnerPositionHelper.GetRandomSpawnPosition();
            return entity;
        }

        public void Kill(T entity)
        {
            spawnerHelper.Kill(entity);
        }

        public void FreeSpawner()
        {
            spawnerHelper.FreeSpawner();
        }
    }
}

