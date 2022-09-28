using BalloonsShooter.Core;
using BalloonsShooter.Core.Events;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace BalloonsShooter.Effects.Managers
{
    public class ExplosionsManager : MonoBehaviour
    {
        [SerializeField]
        private Explosion balloonExplosionPrefab;

        private BalloonsCountSO balloonsCount;
        private ObjectPool<Explosion> explosionsObjectPool;

        private void OnEnable()
        {
            EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void Start()
        {
            balloonsCount = ServiceLocator<BalloonsCountSO>.GetService();
            var maxBalloonsCount = balloonsCount.MaxBalloonsCount;
            explosionsObjectPool = new(
                createFunc: () =>
                {
                    Explosion explosion = Instantiate(balloonExplosionPrefab);
                    explosion.OnExplosionEnded += OnExplosionEnded;
                    return explosion;
                },
                actionOnGet: (Explosion explosion) =>
                {
                    explosion.gameObject.SetActive(true);
                },
                actionOnRelease: (Explosion explosion) =>
                {
                    explosion.gameObject.SetActive(false);
                },
                actionOnDestroy: (Explosion explosion) =>
                {
                    if (explosion.gameObject != null) Destroy(explosion.gameObject);
                },
                collectionCheck: false,
                defaultCapacity: maxBalloonsCount,
                maxSize: maxBalloonsCount * 2
            );
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
        }

        private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
        {
            var explosion = explosionsObjectPool.Get();
            explosion.transform.position = evt.entity.transform.position;
        }

        private void OnExplosionEnded(Explosion explosion)
        {
            explosionsObjectPool.Release(explosion);
        }
    }
}

