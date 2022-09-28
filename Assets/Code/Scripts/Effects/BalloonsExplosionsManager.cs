using BalloonsShooter.Core;
using BalloonsShooter.Effects.Events;
using BalloonsShooter.Gameplay.Archetypes;
using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.ScriptableObjects;
using UnityEngine;

namespace BalloonsShooter.Effects
{
    public class BalloonsExplosionsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject balloonExplosionPrefab;

        private BalloonsCountSO balloonsCount;
        private ObjectPoolHelper<Transform> objectPoolExplosions;

        private void OnEnable()
        {
            EventsManager.AddListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
            EventsManager.AddListener<ExplosionEndedEvent>(OnExplosionEnded);
        }

        private void Start()
        {
            balloonsCount = ServiceLocator<BalloonsCountSO>.GetService();
            var maxBalloonsCount = balloonsCount.GetMaxBalloonsCount();
            objectPoolExplosions = new(
                balloonExplosionPrefab.transform,
                false,
                maxBalloonsCount,
                maxBalloonsCount * 2
            );
        }

        private void OnDisable()
        {
            EventsManager.RemoveListener<EntityClickedEvent<Balloon>>(OnBalloonClicked);
            EventsManager.RemoveListener<ExplosionEndedEvent>(OnExplosionEnded);
        }

        private void OnBalloonClicked(EntityClickedEvent<Balloon> evt)
        {
            var explosion = objectPoolExplosions.pool.Get();
            explosion.transform.position = evt.entity.transform.position;
        }

        private void OnExplosionEnded(ExplosionEndedEvent evt)
        {
            objectPoolExplosions.pool.Release(evt.parent.transform);
        }
    }
}

