using BalloonsShooter.Gameplay.Events;
using BalloonsShooter.Gameplay.Helpers;
using BalloonsShooter.Gameplay.Managers;
using UnityEngine;

namespace BalloonsShooter.Gameplay.Archetypes
{
    public abstract class Actor<T> : MonoBehaviour where T : MonoBehaviour 
    {
        private T actorComponent;

        protected virtual void Awake()
        {
            actorComponent = GetComponent<T>();
            EventsManager.Broadcast(new ComponentCreatedEvent<T>(actorComponent));
        }

        protected virtual void OnEnable()
        {
            EventsManager.Broadcast(new ComponentEnabledEvent<T>(actorComponent));
        }

        protected virtual void OnDisable()
        {
            EventsManager.Broadcast(new ComponentDisabledEvent<T>(actorComponent));
        }

        protected virtual void OnDestroy()
        {
            EventsManager.Broadcast(new ComponentDestroyedEvent<T>(actorComponent));
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckCollisionWithDeathZone(other);
        }

        private void CheckCollisionWithDeathZone(Collider collider)
        {
            if (collider.CompareTag(GameConstants.TAGS_DEATH_ZONE))
            {
                EventsManager.Broadcast(new DeathCollisionEvent<T>(actorComponent));
            }
        }
    }
}

