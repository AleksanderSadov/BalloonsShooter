using BalloonsShooter.Gameplay.Archetypes;
using UnityEngine;
using UnityEngine.Pool;

namespace BalloonsShooter.Gameplay.Helpers
{
    public class BalloonsObjectPool
    {
        public readonly ObjectPool<Balloon> pool;

        private readonly Balloon balloonPrefab;

        public BalloonsObjectPool(Balloon balloonPrefab, bool collectionCheck, int defaultCapacity, int maxCapacity)
        {
            this.balloonPrefab = balloonPrefab;

            pool = new(
                createFunc: CreatePooledItem,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnedToPool,
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: collectionCheck,
                defaultCapacity: defaultCapacity,
                maxSize: maxCapacity
            );
        }

        private Balloon CreatePooledItem()
        {
            Balloon balloon = Object.Instantiate(
                balloonPrefab,
                Vector3.zero,
                balloonPrefab.transform.rotation
            );

            return balloon;
        }

        private void OnTakeFromPool(Balloon balloon)
        {
            balloon.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(Balloon balloon)
        {
            balloon.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(Balloon balloon)
        {
            Object.Destroy(balloon.gameObject);
        }
    }
}

