using UnityEngine;
using UnityEngine.Pool;

namespace BalloonsShooter.Core
{
    public class ObjectPoolHelper<T> where T : MonoBehaviour
    {
        public readonly ObjectPool<T> pool;

        private readonly T objectPrefab;

        public ObjectPoolHelper(T objectPrefab, bool collectionCheck, int defaultCapacity, int maxCapacity)
        {
            this.objectPrefab = objectPrefab;

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

        private T CreatePooledItem()
        {
            T gameComponent = Object.Instantiate(
                objectPrefab,
                Vector3.zero,
                objectPrefab.transform.rotation
            );

            return gameComponent;
        }

        private void OnTakeFromPool(T entity)
        {
            entity.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(T entity)
        {
            entity.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(T entity)
        {
            if (entity != null) Object.Destroy(entity.gameObject);
        }
    }
}

