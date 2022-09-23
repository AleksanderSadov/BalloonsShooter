using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Interfaces;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class BalloonsSpawner
    {
        private readonly BalloonsObjectPool balloonsObjectPool;
        private readonly IRandomSpawnPosition spawnerPositionHelper;

        public BalloonsSpawner(
            Balloon balloonPrefab,
            Transform balloonPlaneSpawner,
            int defaultCapacity,
            int maxCapacity
        )
        {
            spawnerPositionHelper = new PlaneSpawnerPositionVector(balloonPlaneSpawner);

            balloonsObjectPool = new(
                balloonPrefab: balloonPrefab,
                collectionCheck: true,
                defaultCapacity: defaultCapacity,
                maxCapacity: maxCapacity
            );
        }

        ~BalloonsSpawner()
        {
            balloonsObjectPool.pool.Dispose();
        }

        public Balloon Spawn()
        {
            Balloon balloon = balloonsObjectPool.pool.Get();
            balloon.transform.position = spawnerPositionHelper.GetRandomSpawnPosition();

            return balloon;
        }

        public void Kill(Balloon balloon)
        {
            balloonsObjectPool.pool.Release(balloon);
        }
    }
}

